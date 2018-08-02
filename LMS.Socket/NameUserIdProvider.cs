using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace LMS.Socket
{
    public class NameUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User.GetUserId();
        }
    }

    public static class ClaimsExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
