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

        public string Title { get; set; }

        [Display(Name = "Start time")]
        public DateTimeOffset StartTime { get; set; }

        [Display(Name = "End time")]
        public DateTimeOffset EndTime { get; set; }

        [Display(Name = "Examenee results")]
        public ICollection<ExameneeResultDTO> ExameneeResults { get; set; }
    }
}
