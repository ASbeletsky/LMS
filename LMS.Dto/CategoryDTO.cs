using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Title { get; set; }
        [Display(Name = "Amount tasks")]
        public int TasksCount { get; set; }
        [Display(Name = "Parent Category")]
        public int? ParentCategoryId { get; set; }       
        public CategoryDTO ParentCategory { get; set; }
    }
}
