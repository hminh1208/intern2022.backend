using WebApi.Enums;

namespace WebApi.Models.Category
{
    public class CategoryResponseDto
    {
        public CategoryResponseDto(string name, string slug, StatusEnum status, int? parentId)
        {
            Name = name;
            Slug = slug;
            Status = Status;
            ParentId = parentId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Status { get; set; }
        public int? ParentId { get; set; }
    }

    public class ApiResponseDto
    {
        public ApiResponseDto(List<CategoryResponseDto> results, int total)
        {
            Results = results;
            Total = total;
        }
        public List<CategoryResponseDto> Results { get; set; }
        public int Total { get; set; }
    }
}
