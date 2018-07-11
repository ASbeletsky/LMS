using Microsoft.AspNetCore.Identity;

namespace LMS.Data.Models
{
    public class User : IdentityUser
    {
       public int Year { get; set; }
    }
}
