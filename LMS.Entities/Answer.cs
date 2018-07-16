namespace LMS.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Task Task { get; set; }
        public int TaskId { get; set; }
        public int TestId { get; set; }
        public string Content { get; set; }
    }
}
