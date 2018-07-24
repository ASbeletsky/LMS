using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Entities
{
    public class AnswerSheet
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        //public Test Test { get; set; }
        public int TestId { get; set; }
        public DateTime StartTime { get; set; }// we need this to identify answers to same test wroten in deferent periods
        public List<Answers> Answers { get; set; }
    }
}
