using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class AnswerDTO
    {
        public int Id {get;set;}
        [Display(Name ="User")]
        public int UserId { get; set; }
        [Display(Name ="Task")]
        public AnswerDTO Task { get; set; }
        public int TaskId { get; set; }
        [Display(Name ="Test")]
        public int TestId { get; set; }
        [Required(ErrorMessage ="Content must be defined")]
        public string Content { get; set; }
    }
}
