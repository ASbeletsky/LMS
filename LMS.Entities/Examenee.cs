using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Entities
{
    public class Examenee
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        //public Test Test { get; set; }
        public int TestId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<Answers> Answers { get; set; }
    }
}
