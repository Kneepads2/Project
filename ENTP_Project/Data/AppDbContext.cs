using Microsoft.EntityFrameworkCore;
using ENTP_Project.Models;
using Microsoft.Extensions.Configuration;

namespace ENTP_Project.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        /*public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        } */

        /*protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite(Configuration.GetConnectionString("Database"));
        }*/

        public DbSet<UserModel> Users { get; set; }
        public DbSet<WorkoutModel> Workouts { get; set; }
        public DbSet<MealModel> Meals { get; set; }
        //public DbSet<MealModel> MyMeals { get; set; }
        //public DbSet<WorkoutModel> MyWorkouts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.MyMeals)
                .WithOne(m => m.Creator)       
                .HasForeignKey(m => m.UserId) 
                .OnDelete(DeleteBehavior.Cascade);

            // User to Workouts relationship (One-to-Many)
            modelBuilder.Entity<UserModel>()
                .HasMany(u => u.MyWorkouts)
                .WithOne(w => w.Creator)      
                .HasForeignKey(w => w.UserId)  
                .OnDelete(DeleteBehavior.Cascade);

            /*modelBuilder.Entity<MealModel>()
                .HasOne(m => m.Creator)  
                .WithMany(u => u.MyMeals) 
                .HasForeignKey(m => m.UserId); 

            
            modelBuilder.Entity<WorkoutModel>()
                .HasOne(w => w.Creator)  
                .WithMany(u => u.MyWorkouts) 
                .HasForeignKey(w => w.UserId); */
        }
    }
}
