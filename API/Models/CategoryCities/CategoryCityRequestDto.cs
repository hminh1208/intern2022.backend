using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.CategoryCities
{
    public class CategoryCityRequestDto
    {
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        [Required]
        [MaxLength(5)]
        public string ShortName { get; set; }
    }
}
