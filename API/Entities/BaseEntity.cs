using Newtonsoft.Json;

namespace WebApi.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; }    
        public DateTime UpdatedDate { get; set; }
        [JsonIgnore]
        public Account CreatedAccount { get; set; }
        [JsonIgnore]
        public Account UpdatedAccount { get; set; }
        public Guid CreatedAccountId { get; set; }
        public Guid UpdatedAccountId { get; set; }
    }
}
