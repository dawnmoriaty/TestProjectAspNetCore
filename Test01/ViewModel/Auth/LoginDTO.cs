using System.ComponentModel.DataAnnotations;

namespace Test01.ViewModel.Auth
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
