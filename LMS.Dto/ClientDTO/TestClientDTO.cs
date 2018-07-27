using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TestClientDTO
    {
        public TestClientDTO()
        {
            Levels = new List<TestLevelClientDTO>();
        }

        public int Id { get; set; }
        
        [Required(ErrorMessage = "Title must be defined")]
        public string Title { get; set; }

        public CategoryDTO Category { get; set; }

        public IList<TestLevelClientDTO> Levels { get; set; }
    }
}
