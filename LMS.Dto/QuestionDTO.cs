namespace LMS.Dto
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public int Complexity { get; set; }
        public string Content { get; set; }
        
        public bool IsVisible { get; set; }

        public QuestionTypeDTO Type { get; set; }
        public int TypeId { get; set; }
        public CategoryDTO Category { get; set; }
        public int CategoryId { get; set; }
    }
}
