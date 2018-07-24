using System.Collections.Generic;

namespace LMS.Dto
{
    public class TestVariantDTO
    {
        public TestVariantDTO()
        {
            Levels = new List<TestVariantLevelDTO>();
        }

        public int Id { get; set; }

        public TestTemplateDTO TestTemplate { get; set; }
        public int TestTemplateId { get; set; }

        public string Title { get; set; }
        public IList<TestVariantLevelDTO> Levels { get; set; }
    }
}
