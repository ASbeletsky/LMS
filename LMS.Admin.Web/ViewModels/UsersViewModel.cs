using LMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Admin.Web.ViewModels
{
    public class UsersViewModel
    {
        public User user { get; set; }

        public string role { get; set; }
    }
}
