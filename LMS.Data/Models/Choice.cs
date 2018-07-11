using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Data.Models
{
    public class Choice
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public bool IsRight { get; set; }

        public TestProblem Problem { get; set; }
    }
}
