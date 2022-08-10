using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Accounts
{
    public class AuthenticateRefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
