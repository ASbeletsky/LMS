using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class AnswersDTO
    {
        public int Id {get;set;}
        [Display(Name = "Task")]
        public TaskClientDTO Task { get; set; }
        public int TaskId { get; set; }
        [Required(ErrorMessage ="Content must be defined")]
        public string Content { get; set; }
        public double Result { get; set; }
        //[Display(Name = "User's test session")]
        //public TestSessionUser TestSessionUser { get; set; }
        public int TestSessionUserId { get; set; }
    }
}
