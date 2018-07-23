using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Data.Models
{
    class TestResult
    {
        public int Id { get; set; }
        public Test Test { get; set; }
        //USER
        public List<Answer> Answers { get; set; }
        public DateTime TestingDate { get; set; }
    }
}
