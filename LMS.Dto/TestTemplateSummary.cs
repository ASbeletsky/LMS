using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TestTemplateSummary
    {
        public TestTemplateSummary()
        {
            Categories = new List<string>();
            Levels = new List<TestTemplateSummaryLevel>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Avg. Complexity")]
        public double AvgComplexity { get; set; }

        public ICollection<string> Categories { get; set; }

        public ICollection<TestTemplateSummaryLevel> Levels { get; set; }
    }
}
