namespace LMS.Entities
{
    public class LevelTaskType
    {
        public Level Level { get; set; }
        public int LevelId { get; set; }
        
        public TaskType TaskType { get; set; }
        public int TaskTypeId { get; set; }
    }
}
