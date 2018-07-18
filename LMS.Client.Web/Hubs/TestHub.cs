using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Client.Web.Hubs
{
    public class TestHub : Hub
    {
        //for admin
        public async Task SendComand(string user, string comand, string message)
        {
            await Clients.User(user).SendAsync("Task",comand, message);
        }

        //for admin
        // Comands List
        // comand setTimer, message time in ms
        // comand Banne, message none
        // comand stopTimer
        // comand contTimer
        public Task SendComandToGroups(string comand, string message)
        {
            List<string> groups = new List<string>() { "Users" };
            return Clients.Groups(groups).SendAsync("Task",comand, message);
        }

        public Task UsersInNet()
        {
            List<string> Users;
            Clients.Groups("Users")

        }

        public Task SendReportToGroups(string report)
        {
            List<string> groups = new List<string>() { "Users" };
            return Clients.Groups(groups).SendAsync("Report", report);
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Users");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Users");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
