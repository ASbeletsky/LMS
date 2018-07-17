namespace LMS.Entities
{
    public class LevelCategory
    {
        public Level Level { get; set; }
        public int LevelId { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
