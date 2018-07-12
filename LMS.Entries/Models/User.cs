using Microsoft.AspNetCore.Identity;
using System;

namespace LMS.Entries.Models
{
    public class User : IdentityUser
    {

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime Date { get; set; }

        public string Education { get; set; }
    }
}
