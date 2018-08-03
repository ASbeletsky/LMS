using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using LMS.Dto;
using LMS.Identity;

namespace LMS.Socket
{
    public class SessionHub : Hub
    {
        private const string AdminGroup = "Admins";

        private static readonly ConcurrentDictionary<string, SessionUserDTO> users;

        static SessionHub()
        {
            users = new ConcurrentDictionary<string, SessionUserDTO>();
        }

        [Authorize(Roles = "admin, moderator")]
        public Task Ban(int sessionId, string userId)
        {
            return Clients.User(userId).SendAsync(nameof(Ban), sessionId);
        }

        [Authorize]
        public Task UpdateState(TestTasksStateDTO state)
        {
            state = UpdateStateForCurrentUser(state);

            return Clients.Groups(AdminGroup).SendAsync(nameof(UpdateState), state);
        }

        [Authorize]
        public Task Complete(TestTasksStateDTO state)
        {
            state = UpdateStateForCurrentUser(state);

            users.TryRemove(state.UserId, out _);

            return Clients.Groups(AdminGroup).SendAsync(nameof(Complete), state, DateTimeOffset.Now);
        }

        public override async Task OnConnectedAsync()
        {
            if (Context.User.IsInRole(Roles.Admin)
                || Context.User.IsInRole(Roles.Moderator)
                || Context.User.IsInRole(Roles.Reviewer))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");

                if (users.Any())
                {
                    await Clients.Caller.SendAsync("Users", new
                    {
                        Users = users
                    });
                }

                await base.OnConnectedAsync();
            }
            else
            {
                if (!(Context.User.GetUserId() is string userId))
                {
                    throw new UnauthorizedAccessException();
                }

                await Groups.AddToGroupAsync(Context.ConnectionId, "Users");

                var user = users.GetOrAdd(userId,
                    (id) => new SessionUserDTO
                    {
                        Id = id,
                        StartTime = DateTimeOffset.Now
                    });

                await Clients.Group(AdminGroup).SendAsync("UserConnected", user);

                await base.OnConnectedAsync();
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (Context.User.IsInRole(Roles.Admin)
                || Context.User.IsInRole(Roles.Moderator)
                || Context.User.IsInRole(Roles.Reviewer))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Admins");
                await base.OnDisconnectedAsync(exception);
            }
            else
            {
                if (!(Context.User.GetUserId() is string userId))
                {
                    throw new UnauthorizedAccessException();
                }

                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Users");

                if (users.TryGetValue(userId, out var user))
                {
                    await Clients.Group(AdminGroup).SendAsync("UserDisconnected", user);
                }

                await base.OnDisconnectedAsync(exception);
            }
        }

        private TestTasksStateDTO UpdateStateForCurrentUser(TestTasksStateDTO state)
        {
            if (!(Context.User.GetUserId() is string userId))
            {
                throw new UnauthorizedAccessException();
            }

            state.UserId = userId;

            var sessionUser = users.AddOrUpdate(userId,
                (id) => new SessionUserDTO { Id = id, StartTime = DateTimeOffset.Now, TasksState = state },
                (_, user) => { user.TasksState = state; return user; });

            return sessionUser.TasksState;
        }
    }
}
