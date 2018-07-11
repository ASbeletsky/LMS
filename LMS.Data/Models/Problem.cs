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
        public string Problem { get; set; }

        public List<ProblemList> ProblemList { get; set; }
        public List<ChoicesList> ChoicesList { get; set; }
        
    }
}
