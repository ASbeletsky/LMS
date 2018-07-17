using Microsoft.AspNetCore.Identity;

namespace LMS.Entities
{
    public class User : IdentityUser
    {
        public string Name => $"{FirstName} {LastName}";

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
