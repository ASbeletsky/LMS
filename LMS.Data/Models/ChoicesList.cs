using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Client.Web.Models
{
    public class ChoicesList {
        [Key]
        public int ID { get; set; }
        public Problem Problem { get; set; }
        public Choice Choice { get; set; }
    }
}
