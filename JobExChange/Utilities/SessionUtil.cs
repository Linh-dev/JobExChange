using Business.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Business.Utilities
{
    public class SessionUtil
    {
        public static UserInfo GetCurrentUser(ClaimsPrincipal c)
        {
            if (c != null)
            {
                UserInfo user = new UserInfo();

                // Lấy thông tin người dùng từ token
                var userId = c.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Lấy ID người dùng
                var username = c.FindFirst(ClaimTypes.Name)?.Value; // Lấy email người dùng
                var role = c.FindFirst(ClaimTypes.Role)?.Value; // Lấy vai trò người dùng (nếu có)

                user.IdStr = userId;
                user.Username = username;
                user.Role = role;
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
