using LMS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Dto
{
    public class ExameneeDTO
    {
        public int Id { get; set; }
        [Display(Name = "User")]
        public User User { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Test")]
        //public Test Test { get; set; }
        public int TestId { get; set; }
        [Display(Name = "Start time")]
        public DateTime StartTime { get; set; }
        [Display(Name = "End time")]
        public DateTime EndTime { get; set; }
        public IList<AnswersDTO> Answers { get; set; }
    }
}
