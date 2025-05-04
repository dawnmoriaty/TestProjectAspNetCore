using Microsoft.AspNetCore.Identity;
using Test01.Domain;
using Test01.Repositories;
using Test01.ViewModel.Auth;

namespace Test01.Service.impl
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IdentityResult> RegisterAsync(RegisterDTO dto)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = dto.UserName,
                Email = dto.Email,
                FullName = dto.FullName,
                AvatarUrl = null, // Hoặc dùng default path nếu có
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User"); // Gán role mặc định
            }

            return result;
        }

        public async Task<bool> LoginAsync(LoginDTO dto)
        {
            var result = await _signInManager.PasswordSignInAsync(
                dto.UserName,
                dto.Password,
                isPersistent: false,
                lockoutOnFailure: false
            );

            return result.Succeeded;
        }
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
