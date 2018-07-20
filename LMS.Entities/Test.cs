using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Entities
{
    public class Test
    {
        public int Id { get; set; }
        public Category TestCategory { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public List<Task> Problems { get; set; }
    }
}
