using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TestSessionDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Start time")]
        public DateTimeOffset StartTime { get; set; }

        public TimeSpan Duration { get; set; }

        [Display(Name = "End time")]
        public DateTimeOffset EndTime => StartTime + Duration;

        public ICollection<TestDTO> Tests { get; set; }
        public ICollection<ExameneeDTO> Examenees { get; set; }
    }
}
