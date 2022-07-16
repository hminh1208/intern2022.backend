using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public class Event : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(500)]
        public String Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

