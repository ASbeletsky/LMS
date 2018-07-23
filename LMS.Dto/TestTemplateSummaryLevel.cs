using System.Collections.Generic;

namespace LMS.Dto
{
    public class TestTemplateSummaryLevel
    {
        public ICollection<(string Type, int Count)> CountPerTypes { get; set; }
    }
}
