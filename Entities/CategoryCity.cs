using System.ComponentModel.DataAnnotations;
using WebApi.Enums;

namespace WebApi.Entities
{
    public class CategoryCity : BaseEntity
    {
        public CategoryCity()
        {

        }
        public CategoryCity(string newName, string newShortName, Account account)
        {
            this.Name = newName;
            this.ShortName = newShortName;
            this.CreatedAccount = account;
            this.UpdatedAccount = account;
            this.CreatedDate = DateTime.Now;
            this.UpdatedDate = DateTime.Now;
            this.Status = (int)StatusEnum.APPROVED;
        }

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
