using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Client.Web.Models
{
    public class Problem_List
    {
        [Key]
        public int ID { get; set; }
        public Test test { get; set; }
        public Problem problem { get; set; }
        public double worth { get; set; }
    }
}
