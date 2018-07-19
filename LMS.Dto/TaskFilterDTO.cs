using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TaskFilterDTO
    {
        public TaskFilterDTO()
        {
            TaskTypes = new List<TaskTypeDTO>();
            Categories = new List<CategoryDTO>();
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
        
        public ICollection<TaskTypeDTO> TaskTypes { get; set; }

        [Display(Name = "Task types")]
        public ICollection<int> TaskTypeIds
        {
            get => TaskTypes.Select(t => t.Id).ToList();
            set => TaskTypes = value.Select(id => new TaskTypeDTO {Id = id}).ToList();
        }

        public ICollection<CategoryDTO> Categories { get; set; }

        [Display(Name = "Categories")]
        public ICollection<int> CategoryIds
        {
            get => Categories.Select(t => t.Id).ToList();
            set => Categories = value.Select(id => new CategoryDTO {Id = id}).ToList();
        }
    }
}
