namespace LMS.Entities
{
    public class TestLevelTask
    {
        public TestLevel Level { get; set; }
        public int LevelId { get; set; }

        public Task Task { get; set; }
        public int TaskId { get; set; }
    }
}
