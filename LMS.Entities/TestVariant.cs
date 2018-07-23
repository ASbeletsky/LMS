using System.Collections.Generic;

namespace LMS.Entities
{
    public class TestVariant
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public int TestTemplateId { get; set; }
        public ICollection<TestVariantLevel> Levels { get; set; }
    }
}
