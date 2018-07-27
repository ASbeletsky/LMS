using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Dto
{
    public class ExamineeDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int Year { get; set; }

        public string College { get; set; }

        public string Faculty { get; set; }

        public string Specialty { get; set; }

        public int Course { get; set; }

        public string EnglishLevel { get; set; }

        public string Comment { get; set; }
    }
}
