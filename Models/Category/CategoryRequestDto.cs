using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Category
{
    public class CategoryRequestDto
    {
        [Required]
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
