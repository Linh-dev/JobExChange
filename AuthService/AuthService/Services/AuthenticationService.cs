using AuthService.Configurations;
using AuthService.Models;
using AuthService.Repositories;
using Business.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Business.Utilities.Constans;

namespace AuthService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(IUserRepository userRepository, IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            password = EncryptUtil.GetSha512(EncryptUtil.Md5(password) + user.Salt);
            if (user != null && user.PasswordHash == password)
            {
                user.Token = GenerateJwtToken(user);
                return user;
            }
            return null;
        }

        public async Task<User> LoginWithFacebook(string facebookId)
        {
            var user = await _userRepository.GetByProviderAsync(facebookId, (int)ProviderType.FACEBOOK);
            if (user != null)
            {
                user.Token = GenerateJwtToken(user);
                return user;
            }
            return null;
        }

        public async Task<User> LoginWithGoogle(string googleId)
        {
            var user = await _userRepository.GetByProviderAsync(googleId, (int)ProviderType.GOOGLE);
            if (user != null)
            {
                user.Token = GenerateJwtToken(user);
                return user;
            }
            return null;
        }

        public async Task Register(User user)
        {
            user.Salt = Guid.NewGuid().ToString().Replace("-", "");
            user.PasswordHash = EncryptUtil.GetSha512(EncryptUtil.Md5(user.PasswordHash) + user.Salt);
            await  _userRepository.AddAsync(user);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.IdStr),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
