using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TestLevelDTO
    {
        public TestLevelDTO()
        {
            Tasks = new List<TaskDTO>();
            AvailableTasks = new List<TaskDTO>();
        }

        public int Id { get; set; }
        public int TestId { get; set; }
        public int? TestTemplateLevelId { get; set; }

        [Required(ErrorMessage = "Description must be defined")]
        public string Description { get; set; }

        [Display(Name = "Template deleted")]
        public bool TemplateDeleted { get; set; }

        [Display(Name = "Template modified")]
        public bool TemplateModified { get; set; }

        [Display(Name = "Tasks")]
        [Required(ErrorMessage = "Tasks cannot be empty")]
        public ICollection<int> TaskIds
        {
            get => Tasks.Select(t => t.Id).ToList();
            set => Tasks = value.Select(id => new TaskDTO() { Id = id }).ToList();
        }

        public ICollection<TaskDTO> Tasks { get; set; }

        [Display(Name = "Available tasks")]
        public ICollection<TaskDTO> AvailableTasks { get; set; }
    }
}
