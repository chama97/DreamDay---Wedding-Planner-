using Microsoft.EntityFrameworkCore;

namespace Wedding_Planner.Models.Entity
{
    public class WeddingPlannerDbContext : DbContext
    {
        public WeddingPlannerDbContext(DbContextOptions<WeddingPlannerDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<WeddingPlanner> WeddingPlanners { get; set; }
        public DbSet<Couple> Couples { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Optional: Fluent API configuration can go here
        }
    }
}
