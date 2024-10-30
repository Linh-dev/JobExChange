using Business.Models;

namespace AuthService.Repositories
{
    public interface IUserRepository : BaseRepository<UserInfo>
    {
        Task<UserInfo> GetByUsernameAsync(string username);
        Task<UserInfo> GetByProviderAsync(string providerIdStr, int provider);
    }
}