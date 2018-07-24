using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TestVariantDTO
    {
        public TestVariantDTO()
        {
            Levels = new List<TestVariantLevelDTO>();
        }

        public int Id { get; set; }

        [Display(Name = "Test template")]
        public TestTemplateDTO TestTemplate { get; set; }

        public int TestTemplateId { get; set; }

        [Required(ErrorMessage = "Title must be defined")]
        public string Title { get; set; }

        public IList<TestVariantLevelDTO> Levels { get; set; }
    }
}
