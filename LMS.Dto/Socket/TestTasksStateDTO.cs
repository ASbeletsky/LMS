namespace LMS.Dto
{
    public class TestTasksStateDTO
    {
        public string UserId { get; set; }

        public int CurrentNumber { get; set; }

        public int CompletedCount { get; set; }
        public int TotalCount { get; set; }
    }
}
