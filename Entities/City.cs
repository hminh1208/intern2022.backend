using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public class City
    { 
        public City()
        {

        }

        public City(string newName, string newAbbName)
        {
            this.Name = newName;
            this.AbbName = newAbbName;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string AbbName { get; set; }
    }
}
