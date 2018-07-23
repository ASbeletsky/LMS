using System.Collections.Generic;

namespace LMS.Dto
{
    public class TestVariantLevelDTO
    {
        public int Id { get; set; }
        public int TestVariantId { get; set; }

        public string Title { get; set; }

        public TaskFilterDTO Filter { get; set; }

        public ICollection<TaskDTO> Tasks { get; set; }
        public ICollection<TaskDTO> AvailableTasks { get; set; }
    }
}
