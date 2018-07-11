using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Client.Web.Models
{
    public class Choice
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(200)]
        public string Ansver { get; set; }
        public bool IsRight { get; set; }
        public List<ChoicesList> ChoicesList { get; set; }
    }
}
