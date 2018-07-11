using System.Collections.Generic;

namespace LMS.Entities
{
    public class TestProblem
    {
        public int Id { get; set; }
        public double Complexity { get; set; }
        public string Content { get; set; }

        public ProblemType Type { get; set; }
        public List<Choice> Choices { get; set; }        
    }
}
