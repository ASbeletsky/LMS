using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Socet
{
    public class TestHub : Hub
    {
        [Authorize(Roles = "admin, moderator, reviewer")]
        public async Task SendComand(string user, string comand, string message)
        {
            await Clients.User(user).SendAsync("Task", comand, message);
        }

        // Comands List
        // comand setTimer, message time in ms
        // comand Banne, message none
        // comand stopTimer
        // comand contTimer
        [Authorize(Roles = "admin, moderator, reviewer")]
        public Task SendComandToGroups(string comand, string message)
        {
            List<string> groups = new List<string>() { "Users" };
            return Clients.Groups(groups).SendAsync("Task", comand, message);
        }

        [Authorize(Roles = "admin, moderator, reviewer")]
        public Task JoinListeners()
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, "Listeners");
        }

        [Authorize(Roles = "admin, moderator, reviewer")]
        public Task OutListeners()
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, "Listeners");
        }

        public void SendAnswer(int number,string answer)
        {
            //отправка результата в бд с ид вопроса number
        }

        protected void RestoreAnswer()
        {
            //отправка результата из бд пользователю
        }

        public Task SendReportToGroups(string report)
        {
            var groups = new List<string>() { "Listeners" };
            return Clients.Groups(groups).SendAsync("Report", report);
        }

        public Task AdminCheck(string comand)
        {
            return Clients.Caller.SendAsync("Check", comand);
        }

        public override async Task OnConnectedAsync()
        {
            if (Context.User.IsInRole("admin") || Context.User.IsInRole("moderator") || Context.User.IsInRole("reviewer"))
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
            if (Context.User.IsInRole("admin") || Context.User.IsInRole("moderator") || Context.User.IsInRole("reviewer"))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Admins");
                await OutListeners();
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
