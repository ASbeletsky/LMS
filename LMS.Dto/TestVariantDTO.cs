﻿using System.Collections.Generic;

namespace LMS.Dto
{
    public class TestVariantDTO
    {
        public int Id { get; set; }
        public int TestTemplateId { get; set; }

        public string Title { get; set; }
        public ICollection<TestVariantLevelDTO> Levels { get; set; }
    }
}