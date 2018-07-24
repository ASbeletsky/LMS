using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TaskAnswerOptionDTO
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        [Display(Name = "Text")]
        public string Content { get; set; }

        public bool IsCorrect { get; set; }

    }
}
