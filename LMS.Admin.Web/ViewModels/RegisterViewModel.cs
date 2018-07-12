using System.ComponentModel.DataAnnotations;

namespace LMS.Admin.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Surnamee")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password do no match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirn password")]
        public string PasswordConfirm { get; set; }
    }
}
