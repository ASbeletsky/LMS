using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Dto
{
    public class TestSessionUserDTO
    {
        public TestSessionUserDTO()
        {
            Answers = new List<TaskAnswerDTO>();
        }
        public TestSessionDTO Session { get; set; }
        public int SessionId { get; set; }

        public string UserId { get; set; }

        public TestDTO Test { get; set; }
        public int? TestId { get; set; }
        public string Categories {get;set;}
        //public DateTimeOffset StartTime { get; set; }
        //public DateTimeOffset EndTime { get; set; }
        //public bool Ended { get; set; }

        public ICollection<TaskAnswerDTO> Answers { get; set; }
    }
}
