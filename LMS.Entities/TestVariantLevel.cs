using System.Collections.Generic;

namespace LMS.Entities
{
    public class TestVariantLevel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public int TestVariantId { get; set; }
        public ICollection<TestVariantLevelTask> Tasks { get; set; }
    }
}
