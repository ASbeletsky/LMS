using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TestTemplateSummary
    {
        public TestTemplateSummary()
        {
            Categories = new List<string>();
            Tasks = new List<TaskTemplateDTO>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Avg. Complexity")]
        public double AvgComplexity { get; set; }

        public ICollection<string> Categories { get; set; }

        [Display(Name = "Levels")]
        public ICollection<TaskTemplateDTO> Tasks { get; set; }
    }
}
