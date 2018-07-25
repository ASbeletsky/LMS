using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Entities
{
    public class TaskInfo
    {
        public Task OurTask { get; set; }
        public int CurrentQuestionNumber { get; set; }
        public int TaskCount { get; set; }
        public string Category { get; set; }
        public string[] Result { get; set; }
    }
}
