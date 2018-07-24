using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class AnswersDTO
    {
        public int Id {get;set;}
        [Display(Name = "Task")]
        public TaskDTO Task { get; set; }
        public int TaskId { get; set; }
        [Required(ErrorMessage ="Content must be defined")]
        public string Content { get; set; }
    }
}
