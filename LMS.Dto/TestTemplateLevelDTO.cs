using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TestTemplateLevelDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }

        [Display(Name = "Score per task")]
        public double ScorePerTask => MaxScore / Count;
        
        public int Count { get; set; }
        
        [Display(Name = "Max score")]
        public double MaxScore { get; set; }

        [Display(Name = "Valid task count")]
        public int ValidTaskCount { get; set; }

        public TaskFilterDTO Filter { get; set; }     
        
        public int TestTemplateLevelId { get; set; }
    }
}
