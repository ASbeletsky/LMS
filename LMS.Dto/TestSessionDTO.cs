using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TestSessionDTO
    {
        public TestSessionDTO()
        {
            TestIds = new List<int>();
            MemberIds = new List<string>();
        }

        public int Id { get; set; }

        [Required]
        public int TestTemplateId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Start time")]
        public DateTimeOffset StartTime { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Display(Name = "End time")]
        public DateTimeOffset EndTime => StartTime + Duration;

        [Required]
        [Display(Name = "Tests")]
        public ICollection<int> TestIds { get; set; }

        [Required]
        [Display(Name = "Members")]
        public ICollection<string> MemberIds { get; set; }
    }
}
