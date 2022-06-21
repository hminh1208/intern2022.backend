using System.Text.Json.Serialization;

namespace WebApi.Entities;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    public IList<AccountRole> AccountRole { get; set; }
}