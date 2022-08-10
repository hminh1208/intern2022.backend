using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Event
{
    public class EventDto
    {
        [Required]
        [MaxLength(500)]
        public String Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
