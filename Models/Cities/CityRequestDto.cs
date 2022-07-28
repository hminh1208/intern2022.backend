using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Cities
{
    public class CityRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string AbbName { get; set; }
    }
}
