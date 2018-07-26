using System;

namespace LMS.Dto
{
    public class TestSessionDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public TimeSpan Duration { get; set; }

        public DateTimeOffset EndTime => StartTime + Duration;

        //public ICollection<Test> Variants { get; set; }
    }
}
