using Newtonsoft.Json;

namespace WebApi.Entities;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    public List<Account> Accounts { get; set; }
}