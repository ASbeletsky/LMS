using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LMS.Socket
{
    /* Comands List
    * comand setTimer, message time in ms
    * comand Banne, message none
    * comand stopTimer
    * comand contTimer
    */
    public class SessionHub : Hub
    {
        [Authorize(Roles = "admin, moderator, reviewer")]
        public async Task SendComand(string user, string command, string message)
        {
            await Clients.User(user).SendAsync("Task", command, message);
        }

        [Authorize(Roles = "admin, moderator, reviewer")]
        public Task SendCommandToGroups(string command, string message)
        {
            var groups = new List<string>() { "Users" };
            return Clients.Groups(groups).SendAsync("Task", command, message);
        }

        public Task SendReportToGroups(string report)
        {
            var groups = new List<string>() { "Users" };
            return Clients.Groups(groups).SendAsync("Report", report);
        }

        public override async Task OnConnectedAsync()
        {
            if (Context.User.IsInRole("admin")
                || Context.User.IsInRole("moderator")
                || Context.User.IsInRole("reviewer"))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
                await base.OnConnectedAsync();
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "Users");
                await base.OnConnectedAsync();
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (Context.User.IsInRole("admin")
                || Context.User.IsInRole("moderator")
                || Context.User.IsInRole("reviewer"))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Admins");
                await base.OnDisconnectedAsync(exception);
            }
            else
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Users");
                await base.OnDisconnectedAsync(exception);
            }
        }
    }
}
