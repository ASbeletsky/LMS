using System;
using System.Collections.Generic;

namespace LMS.Entities
{
    public class TestSessionUser
    {
        public TestSessionUser() {
            Answers = new List<TaskAnswer>();
        }
        public TestSession Session { get; set; }
        public int SessionId { get; set; }

        public string UserId { get; set; }

        public Test Test { get; set; }
        public int? TestId { get; set; }

        public TimeSpan Duration { get; set; }

        public ICollection<TaskAnswer> Answers { get; set; }

        public bool Equals(TestSessionUser other)
        {
            if (this.SessionId == other.SessionId && this.UserId == other.UserId)
            {
                return true;
            }
            return false;
        }
    }
}
