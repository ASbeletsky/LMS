using System;
using System.Collections.Generic;

namespace LMS.Entities
{
    public class TestSession
    {
        public TestSession()
        {
            Tests = new List<TestSessionTest>();
            Members = new List<TestSessionUser>();
        }

        public int Id { get; set; }

        public string Title { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public TimeSpan Duration { get; set; }

        public ICollection<TestSessionTest> Tests { get; set; }
        public ICollection<TestSessionUser> Members { get; set; }
    }
}
