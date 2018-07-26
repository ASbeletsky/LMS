using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TestDTO
    {
        public TestDTO()
        {
            Levels = new List<TestLevelDTO>();
        }

        public int Id { get; set; }

        [Display(Name = "Test template")]
        public TestTemplateDTO TestTemplate { get; set; }

        [Display(Name = "Test template")]
        public int? TestTemplateId { get; set; }
        
        [Required(ErrorMessage = "Title must be defined")]
        public string Title { get; set; }

        public IList<TestLevelDTO> Levels { get; set; }
    }
}
