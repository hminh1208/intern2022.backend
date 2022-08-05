using WebApi.Enums;
using System.ComponentModel.DataAnnotations;
namespace WebApi.Entities
{
    public class Language : BaseEntity
    {
        public Language()
        {

        }

        public Language(string newName, string newShortName, Account account)
        {
            this.Name = newName;
            this.ShortName = newShortName;
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
        public string ShortName { get; set; }
        public StatusEnum Status { get; set; }
    }

}
