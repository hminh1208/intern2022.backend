using System.ComponentModel.DataAnnotations;
namespace WebApi.Entities
{
    public class Gendermanagement
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
