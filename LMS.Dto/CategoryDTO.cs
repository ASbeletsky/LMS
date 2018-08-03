namespace LMS.Dto
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TasksCount { get; set; }
        public int? ParentCategoryId { get; set; }
        public CategoryDTO ParentCategory { get; set; }
    }
}
