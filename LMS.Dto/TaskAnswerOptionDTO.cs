using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TaskAnswerOptionDTO : IEquatable<TaskAnswerOptionDTO>
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        [Display(Name = "Text")]
        public string Content { get; set; }

        public bool IsCorrect { get; set; }

        public string State
        {
            get => IsCorrect ? "on" : "off";
            set => IsCorrect = value == "on";
        }

        public bool Equals(TaskAnswerOptionDTO x)
        {
            if (x == null)
                return false;
            else if (x.Id == Id && x.TaskId == TaskId && x.Content == Content && x.IsCorrect == IsCorrect)
                return true;
            else
                return false;
        }
    }
}
