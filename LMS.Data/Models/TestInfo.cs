using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Data.Models
{
    public class TaskInfo
    {
        public Task OurTask { get; set; }
        public int CurrentQuestionNumber { get; set; }
        public int TaskCount { get; set; }
    }
}
