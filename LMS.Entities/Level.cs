using System.Collections.Generic;

namespace LMS.Entities
{
    public class Level
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int MaxScore { get; set; }
        
        public int MinComplexity { get; set; }
        public int MaxComplexity { get; set; }
                
        public List<LevelTaskType> TaskTypes { get; set; }
        public List<LevelCategory> Categories { get; set; }
        
        public TestTemplate TestTemplate { get; set; }
        public int TestTemplategId { get; set; }
    }
}
