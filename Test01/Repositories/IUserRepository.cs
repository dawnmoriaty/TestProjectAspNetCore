using Test01.Domain;

namespace Test01.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByUsernameAsync(string username);
        Task<ApplicationUser?> GetByEmailAsync(string email);
        Task<ApplicationUser?> GetByIdAsync(Guid id);
        Task CreateAsync(ApplicationUser user);
    }
}
