using Microsoft.AspNetCore.Identity;
using Test01.ViewModel.Auth;

namespace Test01.Service
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterAsync(RegisterDTO dto);
        Task<bool> LoginAsync(LoginDTO dto);
        Task LogoutAsync();
    }
}
