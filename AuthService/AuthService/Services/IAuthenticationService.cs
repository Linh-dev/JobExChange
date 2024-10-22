using AuthService.Models;

namespace AuthService.Services
{
    public interface IAuthenticationService
    {
        Task<User> Login(string username, string password);
        Task<User> LoginWithFacebook(string facebookId);
        Task<User> LoginWithGoogle(string googleId);
        Task Register(User user);
    }
}
