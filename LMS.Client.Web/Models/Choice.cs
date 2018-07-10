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
        public string ansver { get; set; }
        public bool is_right { get; set; }
        public List<Choices_List> choices_Lists { get; set; }
    }
}
