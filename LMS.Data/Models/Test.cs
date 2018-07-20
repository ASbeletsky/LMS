using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Data.Models
{
    public class Test
    {
        public int Id { get; set; }
        public TestCategory Category { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration {get; set; }
        public List<Task> Problems { get; set; }
    }
}
