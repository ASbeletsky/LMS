using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TaskDTO
    {
        public const int MinComplexity = 1;
        public const int MaxComplexity = 10;

        public TaskDTO()
        {
            OptionAnswers = new List<TaskAnswerOptionDTO>();
        }
       
        public int Id { get; set; }

        public ICollection<TaskAnswerOptionDTO> OptionAnswers { get; set; }

        [Range(MinComplexity, MaxComplexity, ErrorMessage = "Value out of range")]
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

        [Display(Name = "Previous version")]
        public TaskDTO PreviousVersion { get; set; }
    }
}
