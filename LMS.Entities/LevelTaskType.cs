namespace LMS.Entities
{
    public class LevelTaskType
    {
        public TestTemplateLevel TestTemplateLevel { get; set; }
        public int TestTemplateLevelId { get; set; }
        
        public TaskType TaskType { get; set; }
        public int TaskTypeId { get; set; }
    }
}
