using Microsoft.AspNetCore.Identity;

namespace Test01.Config
{
    public static class DatabaseSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            string[] roles = { "Admin", "User" };

            foreach (var roleName in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
                }
            }
        }
    }
}
