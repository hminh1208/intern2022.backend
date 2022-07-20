using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public class CategoryCity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        [Required]
        [MaxLength(5)]
        public string ShortName { get; set; }
        public int Status { get; set; }
    }
}
