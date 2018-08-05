using System;
using System.Collections.Generic;

namespace LMS.Entities
{
    public class TestSessionUser
    {
        public TestSessionUser()
        {
            Answers = new List<TaskAnswer>();
        }

        public TestSession Session { get; set; }
        public int SessionId { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }

        public Test Test { get; set; }
        public int? TestId { get; set; }

        public TimeSpan Duration { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public bool Baned { get; set; }
        public string Code { get; set; }

        public DateTimeOffset LastReviewDate { get; set; }
        public User LastReviewer { get; set; }
        public string LastReviewerId { get; set; }

        public ICollection<TaskAnswer> Answers { get; set; }

        public bool Equals(TestSessionUser other)
        {
            return SessionId == other.SessionId && UserId == other.UserId;
        }
    }
}
