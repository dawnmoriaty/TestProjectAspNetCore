using Microsoft.AspNetCore.Mvc;
using Test01.Service;
using Test01.ViewModel.Auth;

namespace Test01.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["RegisterError"] = "Thông tin không hợp lệ";
                return RedirectToAction("Index", "Home");
            }

            try
            {
                await _userService.RegisterAsync(dto);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["RegisterError"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["LoginError"] = "Thông tin không hợp lệ";
                return RedirectToAction("Index", "Home");
            }

            var success = await _userService.LoginAsync(dto);
            if (success)
                return RedirectToAction("Index", "Home");

            TempData["LoginError"] = "Sai tên đăng nhập hoặc mật khẩu";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
