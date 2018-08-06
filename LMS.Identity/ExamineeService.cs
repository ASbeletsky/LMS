using LMS.Dto;
using System;

namespace LMS.Identity
{
    public class ExamineeService
    {
        public ExamineeDTO GetDefaultExaminee()
        {
            return new ExamineeDTO() { BirthYear = DateTime.Now.Year - 18, Course = 1 };
        }
    }
}
