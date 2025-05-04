namespace Test01.ViewModel.User
{
    public class UpdateProfileDTO
    {
        public string? FullName { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public IFormFile? Avatar { get; set; }
    }
}
