﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TaskFilterDTO
    {
        public TaskFilterDTO()
        {
            TaskTypeIds = new List<int>();
            CategoryIds = new List<int>();
        }
        
        [Display(Name = "Min complexity")]
        public int MinComplexity { get; set; } = TaskDTO.MinComplexity;

        [Display(Name = "Max complexity")]
        public int MaxComplexity { get; set; } = TaskDTO.MaxComplexity;

        [Display(Name = "Complexity range")]
        public string ComplexityRange
        {
            get => $"{MinComplexity},{MaxComplexity}";
            set
            {
                var parts = value.Split(',');
                MinComplexity = int.Parse(parts[0]);
                MaxComplexity = int.Parse(parts[1]);
            }
        }
        
        [Display(Name = "Task types")]
        public IList<int> TaskTypeIds { get; set; }

        [Display(Name = "Categories")]
        public IList<int> CategoryIds { get; set; }
    }
}
