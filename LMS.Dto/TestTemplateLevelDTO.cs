namespace LMS.Dto
{
    public class TestTemplateLevelDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        
        public int Count { get; set; }
        public int Score { get; set; }
        
        public TestTemplateDTO TestTemplate { get; set; }
        public int TestConfigId { get; set; }
        
        public TaskFilterDTO Filter { get; set; }
    }
}
