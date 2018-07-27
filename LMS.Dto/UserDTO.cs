
namespace LMS.Dto
{
    public class UserDTO
    {
        public string Name => $"{FirstName} {LastName}";

        public string Id { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public  string ConcurrencyStamp { get; set; }

        public  string SecurityStamp { get; set; }

        public  string PasswordHash { get; set; }

        public  string NormalizedUserName { get; set; }

        public  string UserName { get; set; }

        
    }
}
