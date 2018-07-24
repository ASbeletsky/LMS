using System.Collections.Generic;

namespace LMS.Entities
{
    public class TestVariant
    {
        public TestVariant()
        {
            Levels = new List<TestVariantLevel>();
        }

        public int Id { get; set; }
        public int? TestTemplateId { get; set; }

        public TestTemplate TestTemplate { get; set; }

        public string Title { get; set; }
        public ICollection<TestVariantLevel> Levels { get; set; }
    }
}
