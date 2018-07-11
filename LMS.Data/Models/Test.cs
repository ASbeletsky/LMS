using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Client.Web.Models
{
    public class Test
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MinLength(2), MaxLength(25)]
        public string Called { get; set; }
        public int Time {get; set; }
        public List<ProblemList> ProblemList { get; set; }
    }
}
