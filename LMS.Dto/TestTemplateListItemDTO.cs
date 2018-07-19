using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TestTemplateListItemDTO
    {
        public TestTemplateListItemDTO()
        {
            Categories = new List<CategoryDTO>();
            Tasks = new List<TaskTemplateDTO>();
        }
        
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Avg. Complexity")]
        public double AvgComplexity { get; set; }

        public IReadOnlyList<CategoryDTO> Categories { get; set; }

        [Display(Name = "Levels")]
        public IReadOnlyList<TaskTemplateDTO> Tasks { get; set; }
    }
}
