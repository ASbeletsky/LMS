using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TestTemplateDTO
    {
        public const int MaxScore = 100;
        public const int MaxTaskPerLevelCount = 100;
     
        public TestTemplateDTO()
        {
            Levels = new List<TestTemplateLevelDTO>();
        }
        
        public int Id { get; set; }
        public string Title { get; set; }
        [Display(Name = "Avg. Complexity")]
        public int AvgComplexity { get; set; }
        
        public TimeSpan Duration { get; set; }
        
        public string Description { get; set; }
        
        public IReadOnlyList<TestTemplateLevelDTO> Levels { get; set; }
    }
}
