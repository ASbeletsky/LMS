using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Data.Models
{
    class Answer
    {
        public int Id { get; set; }
        public TestProblem TestProblem { get; set; }
        public List<Choice> Choices { get; set; }
    }
}
