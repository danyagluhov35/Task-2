using Microsoft.EntityFrameworkCore;

namespace TestTask.Entity
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderUser> OrderUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderUser>()
                        .HasKey(ou => new { ou.UserId, ou.OrderId });

            modelBuilder.Entity<OrderUser>()
                        .HasOne(ou => ou.User)
                        .WithMany(u => u.OrderUsers)
                        .HasForeignKey(ou => ou.UserId);

            modelBuilder.Entity<OrderUser>()
                        .HasOne(ou => ou.Order)
                        .WithMany(o => o.OrderUsers)
                        .HasForeignKey(ou => ou.OrderId);
        }
    }
}
