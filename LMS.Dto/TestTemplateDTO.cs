using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TestTemplateDTO
    {
        public TestTemplateDTO()
        {
            Levels = new List<TestTemplateLevelDTO>();
        }

        public const int MaxLevelScore = 100;
        public const int MaxTaskPerLevelCount = 30;
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Title must be defined")]
        public string Title { get; set; }

        [Display(Name = "Avg. Complexity")]
        public int AvgComplexity { get; set; }

        public TimeSpan Duration { get; set; }

        [Required(ErrorMessage = "Description must be defined")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Levels cannot be empty")]
        public IList<TestTemplateLevelDTO> Levels { get; set; }
    }
}
