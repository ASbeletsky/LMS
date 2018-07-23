using System;

namespace LMS.Entities
{
    public class LevelCategory : IEquatable<LevelCategory>
    {
        public TestTemplateLevel TestTemplateLevel { get; set; }
        public int TestTemplateLevelId { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public bool Equals(LevelCategory other)
        {
            return other != null
                && other.TestTemplateLevelId == TestTemplateLevelId
                && other.CategoryId == CategoryId;
        }
    }
}
