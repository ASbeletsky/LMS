using LMS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Dto
{
    public class AnswerSheetDTO
    {
        public int Id { get; set; }
        [Display(Name = "User")]
        public User User { get; set; }
        public int UserId { get; set; }
        [Display(Name = "Test")]
        //public Test Test { get; set; }
        public int TestId { get; set; }
        [Display(Name = "Start time")]
        public DateTime StartTime { get; set; }
        public IList<AnswersDTO> Answers { get; set; }
    }
}
