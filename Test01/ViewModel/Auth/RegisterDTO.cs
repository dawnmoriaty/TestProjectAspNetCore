using System.ComponentModel.DataAnnotations;

namespace Test01.ViewModel.Auth
{
    public class RegisterDTO
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [MinLength(4)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
