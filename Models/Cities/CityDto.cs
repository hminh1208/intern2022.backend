using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Cities
{
    public class CityDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string AbbName { get; set; }
    }
}
