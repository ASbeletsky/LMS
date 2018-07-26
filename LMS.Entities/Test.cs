using System.Collections.Generic;

namespace LMS.Entities
{
    public class Test
    {
        public Test()
        {
            Levels = new List<TestLevel>();
        }

        public int Id { get; set; }
        public int? TestTemplateId { get; set; }

        public TestTemplate TestTemplate { get; set; }

        public string Title { get; set; }
        public ICollection<TestLevel> Levels { get; set; }
    }
}
