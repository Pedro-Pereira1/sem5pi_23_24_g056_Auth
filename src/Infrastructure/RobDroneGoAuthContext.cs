using Microsoft.EntityFrameworkCore;
using RobDroneGoAuth.Domain.Users;

namespace RobDroneGo.Infrastructure
{
    public class RobDroneGoAuthContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public RobDroneGoAuthContext(DbContextOptions<RobDroneGoAuthContext> options) : base(options)
        {
        }
    }
}