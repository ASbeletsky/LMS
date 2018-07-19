using System;
using System.Collections.Generic;

namespace LMS.Entities
{
    public class TestTemplate
    {
        public TestTemplate()
        {
            Levels = new List<TestTemplateLevel>();
        }
        
        public int Id { get; set; }
        public string Title { get; set; }
        
        public TimeSpan Duration { get; set; }
        
        public string Description { get; set; }
        
        public ICollection<TestTemplateLevel> Levels { get; set; }
    }
}
