namespace LMS.Entities
{
    public class TaskAnswerOption
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
    }
}
