using System;
using System.Collections.Generic;

namespace LMS.Entities
{
    public class TestTemplateLevel : IEquatable<TestTemplateLevel>
    {
        public TestTemplateLevel()
        {
            TaskTypes = new List<LevelTaskType>();
            Categories = new List<LevelCategory>();
        }
        
        public int Id { get; set; }
        public string Description { get; set; }
        
        public int Count { get; set; } 
        public double MaxScore { get; set; }
        
        public int MinComplexity { get; set; }
        public int MaxComplexity { get; set; }

        public ICollection<LevelTaskType> TaskTypes { get; set; }
        public ICollection<LevelCategory> Categories { get; set; }
        
        public int TestTemplateId { get; set; }

        public bool Equals(TestTemplateLevel other)
        {
            return Id == other?.Id;
        }
    }
}
