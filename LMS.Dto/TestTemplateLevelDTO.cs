using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TestTemplateLevelDTO
    {
        public TestTemplateLevelDTO()
        {
            Filter = new TaskFilterDTO();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Description must be defined")]
        public string Description { get; set; }

        [Display(Name = "Score per task")]
        public double ScorePerTask => MaxScore / Count;

        [Range(1, TestTemplateDTO.MaxTaskPerLevelCount, ErrorMessage = "Value out of range")]
        public int Count { get; set; } = 1;

        [Display(Name = "Max score")]
        [Range(1, TestTemplateDTO.MaxLevelScore, ErrorMessage = "Value out of range")]
        public double MaxScore { get; set; } = 1;

        [Display(Name = "Valid task count")]
        public int ValidTaskCount { get; set; }

        public TaskFilterDTO Filter { get; set; }     
    }
}
