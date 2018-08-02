using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class ExameneeResultDTO
    {
        public ExameneeResultDTO()
        {
            Answers = new List<TaskAnswerDTO>();
        }

        [Display(Name = "Test title")]
        public string TestTitle { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }

        public TimeSpan Duration { get; set; }

        [Display(Name = "Total score")]
        public double TotalScore { get; set; }

        public int? TestId { get; set; }

        public IList<TaskAnswerDTO> Answers { get; set; }
    }
}
