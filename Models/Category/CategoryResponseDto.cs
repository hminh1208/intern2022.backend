using WebApi.Enums;

namespace WebApi.Models.Category
{
    public class CategoryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public StatusEnum Status { get; set; }
        public int? ParentId { get; set; }
    }
}
