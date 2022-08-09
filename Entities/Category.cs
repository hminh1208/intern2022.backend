using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Enums;

namespace WebApi.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {

        }

        public Category(string name, int? parentId, Account currentAccount)
        {
            this.Name = name;
            this.ParentId = parentId;
            this.CreatedAccount = currentAccount;
            this.UpdatedAccount = currentAccount;
            this.CreatedDate = DateTime.Now;
            this.UpdatedDate = DateTime.Now;
            this.Status = StatusEnum.APPROVED;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Slug { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public List<Category> SubCategories { get; set; }
        public StatusEnum Status { get; set; }
    }
}
