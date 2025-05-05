using Microsoft.AspNetCore.Identity;
using Test01.Domain;
using Test01.ViewModel.Auth;

namespace Test01.Service.impl
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ILogger<UserService> _logger;
        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole<Guid>> roleManager, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        public async Task<IdentityResult> RegisterAsync(RegisterDTO dto)
        {
            _logger.LogInformation("Starting registration process for user: {Username}", dto.UserName);

            // Check if username is already taken
            var existingUser = await _userManager.FindByNameAsync(dto.UserName);
            if (existingUser != null)
            {
                _logger.LogWarning("Username {Username} is already taken", dto.UserName);
                throw new Exception("Tên đăng nhập đã tồn tại");
            }

            // Check if email is already taken
            existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                _logger.LogWarning("Email {Email} is already taken", dto.Email);
                throw new Exception("Email đã tồn tại");
            }

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = dto.UserName,
                Email = dto.Email,
                FullName = dto.FullName,
                AvatarUrl = "default-avatar.jpg",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _logger.LogInformation("Creating user {Username} with ID {UserId}", user.UserName, user.Id);
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                _logger.LogError("Failed to create user {Username}. Errors: {Errors}", user.UserName, errors);
                throw new Exception("Lỗi khi đăng ký: " + errors);
            }

            // Check if the User role exists before adding
            if (await _roleManager.RoleExistsAsync("User"))
            {
                _logger.LogInformation("Adding user {Username} to 'User' role", user.UserName);
                var roleResult = await _userManager.AddToRoleAsync(user, "User");

                if (!roleResult.Succeeded)
                {
                    var errors = string.Join("; ", roleResult.Errors.Select(e => e.Description));
                    _logger.LogWarning("Failed to add user to role. Errors: {Errors}", errors);
                    // We don't throw an exception here since the user was created successfully
                }
            }
            else
            {
                _logger.LogWarning("Role 'User' does not exist. User {Username} was created without a role", user.UserName);
            }

            _logger.LogInformation("User {Username} registration completed successfully", user.UserName);
            return result;
        }

        public async Task<bool> LoginAsync(LoginDTO dto)
        {
            _logger.LogInformation("Attempting login for user: {Username}", dto.UserName);

            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
            {
                _logger.LogWarning("Login failed: Username {Username} not found", dto.UserName);
                return false;
            }

            var result = await _signInManager.PasswordSignInAsync(
                dto.UserName,
                dto.Password,
                isPersistent: false,
                lockoutOnFailure: false
            );

            if (result.Succeeded)
            {
                _logger.LogInformation("User {Username} logged in successfully", dto.UserName);
            }
            else
            {
                _logger.LogWarning("Login failed for user {Username}", dto.UserName);
            }

            return result.Succeeded;
        }

        public async Task LogoutAsync()
        {
            _logger.LogInformation("User logout requested");
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out successfully");
        }

    }
    }
