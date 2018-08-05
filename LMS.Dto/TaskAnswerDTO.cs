using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TaskAnswerDTO
    {
        public int Id { get; set; }

        [Display(Name = "Task")]
        public TaskDTO Task { get; set; }

        public int TaskId { get; set; }

        [Required(ErrorMessage = "Content must be defined")]
        public object Content { get; set; }

        public double Score { get; set; }

        public int TestSessionUserId { get; set; }
    }
}
