using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public class CategoryCity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ShortName { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
