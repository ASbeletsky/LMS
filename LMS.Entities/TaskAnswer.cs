namespace LMS.Entities
{
    public class TaskAnswer
    {
        public int Id { get; set; }
        public Task Task { get; set; }
        public int TaskId { get; set; }
        public string Content { get; set; }
        public double Score { get; set; }
        public TestSessionUser TestSessionUser { get; set; }
    }
}
