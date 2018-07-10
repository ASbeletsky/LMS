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
        public string called { get; set; }
        public int time {get; set; }
        public List<Problem_List> quizes { get; set; }
    }
}
