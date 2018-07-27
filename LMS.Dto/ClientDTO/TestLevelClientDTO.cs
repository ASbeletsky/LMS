using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TestLevelClientDTO
    {
        public TestLevelClientDTO()
        {
            Tasks = new List<TaskClientDTO>();
            //AvailableTasks = new List<TaskDTO>();
        }

        public int Id { get; set; }
        public int TestId { get; set; }

        [Required(ErrorMessage = "Description must be defined")]
        public string Description { get; set; }

        [Display(Name = "Tasks")]
        [Required(ErrorMessage = "Tasks cannot be empty")]
        //public ICollection<int> TaskIds
        //{
        //    get => Tasks.Select(t => t.Id).ToList();
        //    set => Tasks = value.Select(id => new TaskClientDTO() { Id = id }).ToList();
        //}

        public ICollection<TaskClientDTO> Tasks { get; set; }

        //[Display(Name = "Available tasks")]
        //public ICollection<TaskDTO> AvailableTasks { get; set; }
    }
}
