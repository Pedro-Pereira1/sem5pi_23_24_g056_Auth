using Microsoft.EntityFrameworkCore;
using RobDroneGoAuth.Domain.Users;
using RobDroneGoAuth.Infrastructure.Users;

namespace RobDroneGo.Infrastructure
{
    public class RobDroneGoAuthContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public RobDroneGoAuthContext(DbContextOptions<RobDroneGoAuthContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}