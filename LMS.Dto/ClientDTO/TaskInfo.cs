using LMS.Dto;
using System;
using System.Collections.Generic;
using System.Text;


namespace LMS.Dto
{
    public class TaskInfo
    {
        public TaskDTO OurTask { get; set; }
        public int CurrentQuestionNumber { get; set; }
        public int TaskCount { get; set; }
        public string Category { get; set; }
        public string[] Result { get; set; }
    }
}
