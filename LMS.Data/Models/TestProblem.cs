using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Data.Models
{
    public class TestProblem
    {
        public int Id { get; set; }
        public double Complexity { get; set; }
        public string Content { get; set; }

        public ProblemType Type { get; set; }
        public Test Test { get; set; }
        public List<Choice> Choices { get; set; }        
    }
}
