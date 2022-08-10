using System.ComponentModel.DataAnnotations;
namespace WebApi.Models.Languages
{
    public class LanguageRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ShortName { get; set; }
    }
}
