using System.Collections.Generic;

namespace LMS.Dto
{
    public class LevelDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int MaxScore { get; set; }

        public int MinComplexity { get; set; }
        public int MaxComplexity { get; set; }

        public IReadOnlyList<TaskTypeDTO> TaskTypes { get; set; }
        public IReadOnlyList<CategoryDTO> Categories { get; set; }
        
        public TestTemplateDTO TestTemplate { get; set; }
        public int TestConfigId { get; set; }

        public int ValidTaskCount { get; set; }
    }
}
