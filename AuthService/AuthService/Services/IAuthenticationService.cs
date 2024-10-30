using Business.Models;

namespace AuthService.Services
{
    public interface IAuthenticationService
    {
        Task<UserInfo> Login(string username, string password);
        Task<UserInfo> LoginWithFacebook(string facebookId);
        Task<UserInfo> LoginWithGoogle(string googleId);
        Task Register(UserInfo user);
    }
}
