using Microsoft.EntityFrameworkCore;
using ENTP_Project.Models;

namespace ENTP_Project.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        
        public DbSet<UserModel> Users { get; set; }
        public DbSet<WorkoutModel> Workouts { get; set; }
        public DbSet<MealModel> Meals { get; set; }

        // Optionally, configure relationships and constraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // If needed, add further configuration for your tables here
            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.MyMeals)
                .WithOne(m => m.Creator)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.MyWorkouts)
                .WithOne(w => w.Creator)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
