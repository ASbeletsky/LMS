using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TaskDTO
    {
        public int Id { get; set; }

        [Range(0, 10, ErrorMessage = "Value out of range")]
        public int Complexity { get; set; }

        [Required(ErrorMessage = "Content must be defined")]
        public string Content { get; set; }

        [Display(Name = "Is active")]
        public bool IsActive { get; set; }

        public TaskTypeDTO Type { get; set; }

        [Display(Name = "Type")]
        public int TypeId { get; set; }

        public CategoryDTO Category { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}
