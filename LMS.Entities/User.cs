using Microsoft.AspNetCore.Identity;

namespace LMS.Entities
{
    public class User : IdentityUser
    {
        public string Name => string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName)
            ? UserName
            : $"{FirstName} {LastName}".Trim();

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
