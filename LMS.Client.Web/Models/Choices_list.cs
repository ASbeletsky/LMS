using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Client.Web.Models
{
    public class Choices_List {
        [Key]
        public int ID { get; set; }
        public Problem problem { get; set; }
        public Choice choice { get; set; }
    }
}
