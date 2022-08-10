using System.ComponentModel.DataAnnotations;
using WebApi.Enums;

namespace WebApi.Entities
{
    public class City : BaseEntity
    { 
        public City()
        {

        }

        public City(string newName, string newAbbName, Account account)
        {
            this.Name = newName;
            this.AbbName = newAbbName;
            this.CreatedAccount = account;
            this.UpdatedAccount = account;
            this.CreatedDate = DateTime.Now;
            this.UpdatedDate = DateTime.Now;
            this.Status = StatusEnum.APPROVED;
        }


        [Key]
        public int Id { get; set; }
        [MaxLength(500)]
        public string Name { get; set; }
        [MaxLength(10)]
        public string AbbName { get; set; }
        public StatusEnum Status { get; set; }
    }
}
