using Microsoft.AspNetCore.Identity;

namespace Test01.Domain
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string? AvatarUrl { get; set; }
        // á shiba mẹ chưa bao h phải làm thủ công như này
        public DateTime CreatedAt { get; set; }    // kế thừa "thủ công"
        public DateTime UpdatedAt { get; set; }
    }
}
