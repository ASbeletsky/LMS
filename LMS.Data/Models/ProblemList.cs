using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Client.Web.Models
{
    public class ProblemList
    {
        [Key]
        public int ID { get; set; }
        public Test Test { get; set; }
        public Problem Problem { get; set; }
        public double Worth { get; set; }
    }
}
