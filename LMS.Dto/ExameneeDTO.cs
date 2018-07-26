namespace LMS.Dto
{
    public class ExameneeDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public TestSessionDTO Session { get; set; }

        public int SessionId { get; set; }
    }
}
