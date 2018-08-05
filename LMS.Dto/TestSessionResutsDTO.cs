using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TestSessionResultsDTO
    {
        public TestSessionResultsDTO()
        {
            ExameneeResults = new List<ExameneeResultDTO>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Start time")]
        public DateTimeOffset StartTime { get; set; }

        [Display(Name = "End time")]
        public DateTimeOffset EndTime { get; set; }

        public bool IsActive => StartTime < DateTimeOffset.Now && !IsEnded;

        public bool IsEnded => DateTimeOffset.Now >= EndTime;

        [Display(Name = "Examenee results")]
        public ICollection<ExameneeResultDTO> ExameneeResults { get; set; }
    }
}
