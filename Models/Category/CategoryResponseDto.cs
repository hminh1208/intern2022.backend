namespace WebApi.Models.Category
{
    public class CategoryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Status { get; set; }
        public int? ParentId { get; set; }
    }
}
