using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using LMS.Dto;
using LMS.Identity;
using LMS.Business.Services;

namespace LMS.Socket
{
    public class SessionHub : Hub
    {
        private const string AdminGroup = "Admins";

        private static readonly ConcurrentDictionary<string, SessionUserDTO> users =
            new ConcurrentDictionary<string, SessionUserDTO>();
        private readonly AnswerService answerService;
        private readonly TestSessionUserService testSessionUserService;

        public SessionHub(AnswerService _answerService,TestSessionUserService _testSessionUserService)
        {
            answerService = _answerService;
            testSessionUserService = _testSessionUserService;
        }

        [Authorize(Roles = "admin, moderator")]
        public Task Ban(int sessionId, string userId)
        {
            return Clients.User(userId).SendAsync(nameof(Ban), sessionId);
        }

        public void SendAnswer(int sessionId, int number, string answer)
        {
            var answerDto = new TaskAnswerDTO()
            {
                TaskId = number,
                Content = answer,
                TestSessionUserSessionId = sessionId,
                TestSessionUserUserId = "2524e8bf-67a7-4693-ba3b-8c1a7d60fb2a"
            };
            answerService.UpdateAsync(answerDto);
        }

        [Authorize]
        public Task UpdateState(TestTasksStateDTO state)
        {
            var user = UpdateStateForCurrentUser(state);

            return Clients.Groups(AdminGroup).SendAsync(nameof(UpdateState), user);
        }

        [Authorize]
        public Task Complete(TestTasksStateDTO state)
        {
            var user = UpdateStateForCurrentUser(state);
            user.StartTime = user.StartTime ?? DateTimeOffset.Now;
            user.Duration = DateTimeOffset.Now - user.StartTime.Value;

            return Clients.Groups(AdminGroup).SendAsync(nameof(Complete), user);
        }

        [Authorize]
        public Task Start()
        {
            if (!(Context.User.GetUserId() is string userId))
            {
                throw new UnauthorizedAccessException();
            }

            var user = users.AddOrUpdate(userId,
                (id) => new SessionUserDTO
                {
                    Id = id,
                    StartTime = DateTimeOffset.Now
                },
                (_, u) =>
                {
                    u.StartTime = DateTimeOffset.Now;
                    u.Duration = null;
                    return u;
                });

            return Clients.Groups(AdminGroup).SendAsync(nameof(Start), user);
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
                    await Clients.Caller.SendAsync("Users", users.Values);
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
                    (id) => new SessionUserDTO { Id = id });

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

        private SessionUserDTO UpdateStateForCurrentUser(TestTasksStateDTO state)
        {
            if (!(Context.User.GetUserId() is string userId))
            {
                throw new UnauthorizedAccessException();
            }
            if (!users.TryGetValue(userId, out var user))
            {
                throw new InvalidOperationException("Attempt to update not connected user");
            }
            user.TasksState = state;

            users.TryUpdate(userId, user, user);

            return user;
        }
    }
}
