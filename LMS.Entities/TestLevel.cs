using System.Collections.Generic;

namespace LMS.Entities
{
    public class TestLevel
    {
        public TestLevel()
        {
            Tasks = new List<TestLevelTask>();
        }

        public int Id { get; set; }
        public int TestId { get; set; }
        public int? TestTemplateLevelId { get; set; }

        public string Description { get; set; }
        public ICollection<TestLevelTask> Tasks { get; set; }
    }
}
