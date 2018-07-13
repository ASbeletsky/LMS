namespace LMS.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public Complexity Complexity { get; set; }
        public string Content { get; set; }

        public bool IsVisible { get; set; }

        public QuestionType Type { get; set; }
        public int TypeId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
