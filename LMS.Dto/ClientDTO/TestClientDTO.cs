using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TestClientDTO
    {
        public TestClientDTO()
        {
            Tasks = new List<TaskClientDTO>();
            Categories = new List<CategoryDTO>();
        }

        public int Id { get; set; }
        
        [Required(ErrorMessage = "Title must be defined")]
        public string Title { get; set; }

        public IList<CategoryDTO> Categories { get; set; }

        public IList<TaskClientDTO> Tasks { get; set; }
    }
}
