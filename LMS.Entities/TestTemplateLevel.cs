using System.Collections.Generic;

namespace LMS.Entities
{
    public class TestTemplateLevel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        
        public int Count { get; set; } 
        public int Score { get; set; }
        
        public TestTemplate TestTemplate { get; set; }
        public int TestTemplategId { get; set; }
        
        public TaskFilter Filter { get; set; }
    }
}
