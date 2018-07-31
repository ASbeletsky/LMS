
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class UserDTO
    {
        public UserDTO()
        {
            Roles = new List<string>();
        }

        public string Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password do not match")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public  string ConcurrencyStamp { get; set; }

        public  string SecurityStamp { get; set; }

        public  string PasswordHash { get; set; }

        public  string NormalizedUserName { get; set; }

        [Required]
        public  string UserName { get; set; }

        public ExamineeDTO Examinee { get; set; }

        [Required]
        public ICollection<string> Roles { get; set; }

        public string Name => $"{FirstName} {LastName}";
    }
}
