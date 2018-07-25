namespace LMS.Entities
{
    public class Answers
    {
        public int Id { get; set; }
        public Task Task { get; set; }
        public int TaskId { get; set; }
        public string Content { get; set; }
        public double Result { get; set; }

        public int ExameneeId { get; set; }
    }
}
