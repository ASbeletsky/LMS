namespace LMS.Entities
{
    public class Examenee
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public Test Test { get; set; }
        public int TestId { get; set; }

        public TestSession Session { get; set; }
        public int SessionId { get; set; }
    }
}
