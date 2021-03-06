﻿using System.Collections.Generic;

namespace LMS.Entities
{
    public class Task
    {
        public Task()
        {
            AnswerOptions = new List<TaskAnswerOption>();
        }
        public IList<TaskAnswerOption> AnswerOptions { get; set; }

        public int Id { get; set; }
        public int Complexity { get; set; }
        public string Content { get; set; }

        public bool IsActive { get; set; }

        public TaskType Type { get; set; }
        public int TypeId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        
        public Task PreviousVersion { get; set; }
    }
}
