using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Test01.Domain;

namespace Test01.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //public DbSet<Workout> Workouts { get; set; }  // sau này dùng cho bài tập
        //public DbSet<Schedule> Schedules { get; set; } // sau này dùng cho lịch tập

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure CreatedAt/UpdatedAt tự động
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entity.ClrType))
                {
                    builder.Entity(entity.ClrType)
                           .Property(nameof(BaseEntity.CreatedAt))
                           .HasDefaultValueSql("getutcdate()");

                    builder.Entity(entity.ClrType)
                           .Property(nameof(BaseEntity.UpdatedAt))
                           .HasDefaultValueSql("getutcdate()");
                }
            }
        }
    }
}
