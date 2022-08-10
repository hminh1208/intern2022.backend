using System.ComponentModel.DataAnnotations;
using WebApi.Enums;
namespace WebApi.Entities
{
    public class Gendermanagement : BaseEntity
    {
        public Gendermanagement()
        {

        }

        public Gendermanagement(string newName, Account account)
        {
            this.Name = newName;
            this.CreatedAccount = account;
            this.UpdatedAccount = account;
            this.CreatedDate = DateTime.Now;
            this.UpdatedDate = DateTime.Now;
            this.Status = StatusEnum.APPROVED;
        }


        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }

        public StatusEnum Status { get; set; }
    }
}

