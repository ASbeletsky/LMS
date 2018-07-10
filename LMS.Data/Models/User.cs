using Microsoft.AspNetCore.Identity;

namespace LMS.Data.Models
{
    public class User : IdentityUser
    {
       public string Year { get; set; }
    }
}
