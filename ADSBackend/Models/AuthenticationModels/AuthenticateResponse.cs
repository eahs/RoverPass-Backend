using ADSBackend.Models;
using ADSBackend.Models.Identity;

namespace ADSBackend.Models.AuthenticationModels
{
    public class AuthenticateResponse
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(ApplicationUser user, string token)
        {
            UserId = user.Id;
            Email = user.Email;
            Token = token;
        }
    }
}
