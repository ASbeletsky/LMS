using System.Collections.Generic;

namespace LMS.Entities
{
    public class TestVariantLevel
    {
        public int Id { get; set; }
        public int TestVariantId { get; set; }
        public int? TestTemplateLevelId { get; set; }

        public string Description { get; set; }
        public ICollection<TestVariantLevelTask> Tasks { get; set; }
    }
}
