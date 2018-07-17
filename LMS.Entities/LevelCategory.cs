namespace LMS.Entities
{
    public class LevelCategory
    {
        public TestTemplateLevel TestTemplateLevel { get; set; }
        public int TestTemplateLevelId { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
