using System;
using System.Collections.Generic;

namespace LMS.Data.Models
{
    public class Test
    {
        public int Id { get; set; }
        public TestCategory Category { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration {get; set; }
        public List<TestProblem> Problems { get; set; }
    }
}
