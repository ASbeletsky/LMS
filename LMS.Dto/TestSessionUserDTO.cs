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

        public TimeSpan Duration { get; set; }

        public ICollection<TaskAnswerDTO> Answers { get; set; }
    }
}
