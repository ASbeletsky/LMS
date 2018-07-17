using System.Collections.Generic;

namespace LMS.Dto
{
    public class TestTemplateDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Complexity { get; set; }
        public IReadOnlyList<CategoryDTO> Categories { get; set; }
    }
}

