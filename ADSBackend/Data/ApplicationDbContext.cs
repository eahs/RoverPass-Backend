using ADSBackend.Models;
using ADSBackend.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ADSBackend.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ConfigurationItem> ConfigurationItem { get; set; }
        public DbSet<Pass> Pass { get; set; }
        public DbSet<PassType> PassType { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<Period> Period { get; set; }
        public DbSet<ReportType> ReportType { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<Class>()
                .HasIndex(c => c.JoinCode)
                .IsUnique();

        }

        public DbSet<ADSBackend.Models.RestrictedRoom> RestrictedRoom { get; set; }

        public DbSet<ADSBackend.Models.ReportStudent> ReportStudent { get; set; }

        public DbSet<ADSBackend.Models.Timer> Timer { get; set; }





   
    }
}
