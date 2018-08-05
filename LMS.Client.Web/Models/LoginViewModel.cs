using System.ComponentModel.DataAnnotations;

namespace LMS.Client.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }
    }
}
