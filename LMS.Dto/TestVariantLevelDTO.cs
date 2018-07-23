using System.Collections.Generic;

namespace LMS.Dto
{
    public class TestVariantLevelDTO
    {
        public int Id { get; set; }
        public int TestVariantId { get; set; }
        public int? TestTemplateLevelId { get; set; }

        public string Description { get; set; }

        public bool TemplateDeleted { get; set; }
        public bool TemplateModified { get; set; }

        public TaskFilterDTO Filter { get; set; }

        public ICollection<TaskDTO> Tasks { get; set; }
        public ICollection<TaskDTO> AvailableTasks { get; set; }
    }
}
