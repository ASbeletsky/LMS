using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TestClientDTO
    {
        public TestClientDTO()
        {
            Tasks = new List<TaskDTO>();
        }

        public int Id { get; set; }
        
        [Required(ErrorMessage = "Title must be defined")]
        public string Title { get; set; }

        public System.DateTimeOffset EndTime { get; set; }

        public IList<TaskDTO> Tasks { get; set; }
    }
}
