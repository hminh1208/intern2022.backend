using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string AbbName { get; set; }
    }
}
