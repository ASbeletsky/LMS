using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Client.Web.Models
{
    public class Problem
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(500), Required]
        public string problem { get; set; }

        public List<Problem_List> problem_Lists { get; set; }
        public List<Choices_List> choices_Lists { get; set; }
        
    }
}
