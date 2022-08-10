using System.ComponentModel.DataAnnotations;
namespace WebApi.Models.Gendermanagements
{
    public class GenderRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
