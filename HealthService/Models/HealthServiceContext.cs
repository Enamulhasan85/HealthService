using HealthService.DataAccess;
using HealthService.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace HealthService.cs
{
    public class HealthServiceContext : DbContext
    {

        public HealthServiceContext() : base("HealthServiceContext")
        {
        }

        public DbSet<Patient> Patient { get; set; }
        public DbSet<Disease> Disease { get; set; }
        public DbSet<Upazilla> Upazilla { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<User>()
                    .HasMany(u => u.Roles)
                    .WithMany(r => r.Users)
                    .Map(m =>
                    {
                        m.ToTable("UserRoles");
                        m.MapLeftKey("UserId");
                        m.MapRightKey("RoleId");
                    });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}