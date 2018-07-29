using System.Collections.Generic;

namespace LMS.Entities
{
    public class TestSessionUser
    {
        public int Id { get; set; }
        public TestSessionUser() {
            Answers = new List<Answers>();
        }
        public TestSession Session { get; set; }
        public int SessionId { get; set; }

        public string UserId { get; set; }

        public Test Test { get; set; }
        public int? TestId { get; set; }

        public ICollection<Answers> Answers { get; set; }
    }
}
