using System.Collections.Generic;
using LMS.Dto;
using LMS.Entities;

namespace LMS.Admin.Web.Models
{
    public class EditOrCreateQuestionViewModel
    {
        public IEnumerable<QuestionTypeDTO> AvailableTypes { get; set; }
        public IEnumerable<CategoryDTO> AvailableCategories { get; set; }

        public int Id { get; private set; }
        public Complexity Complexity { get; set; }
        public string Content { get; set; }

        public bool IsVisible { get; private set; }

        public int TypeId { get; set; }
        public int CategoryId { get; set; }
    }
}
