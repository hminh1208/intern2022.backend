namespace WebApi.Models.Accounts;

using System.Text.Json.Serialization;
using WebApi.Entities;

public class AuthenticateResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public List<Role> Role { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public bool IsVerified { get; set; }
    public string JwtToken { get; set; }
    [JsonIgnore] // refresh token is returned in http only cookie
    public string RefreshToken { get; set; }
}