using System.Collections.Generic;
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
        [Required(ErrorMessage = "ComplexityRange must be defined")]
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
        [Required(ErrorMessage = "TaskTypes cannot be empty")]
        public ICollection<int> TaskTypeIds { get; set; }

        [Display(Name = "Categories")]
        [Required(ErrorMessage = "Categories cannot be empty")]
        public ICollection<int> CategoryIds { get; set; }
    }
}
