using AuthService.Models;

namespace AuthService.Repositories
{
    public interface IUserRepository : BaseRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByProviderAsync(string providerIdStr, int provider);
    }
}