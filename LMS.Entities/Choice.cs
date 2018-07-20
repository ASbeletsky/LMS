using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Entities
{
    public class Choice
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public bool IsRight { get; set; }

        public Task Problem { get; set; }
    }
}
