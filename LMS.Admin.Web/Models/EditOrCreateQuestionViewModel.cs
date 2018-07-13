using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LMS.Dto;

namespace LMS.Admin.Web.Models
{
    public class EditOrCreateQuestionViewModel
    {
        public IEnumerable<QuestionTypeDTO> AvailableTypes { get; set; }
        public IEnumerable<CategoryDTO> AvailableCategories { get; set; }

        public int Id { get; private set; }
        
        [Range(0, 10, ErrorMessage = "Value out of range")]
        public int Complexity { get; set; }

        [Required(ErrorMessage = "Content must be defined")]
        public string Content { get; set; }

        public bool IsVisible { get; private set; }

        [Display(Name = "Type")]
        public int TypeId { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}
