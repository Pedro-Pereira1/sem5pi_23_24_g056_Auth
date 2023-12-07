using DDDSample1.Infrastructure.Shared;
using RobDroneGo.Infrastructure;
using RobDroneGoAuth.Domain.Users;

namespace RobDroneGoAuth.Infrastructure.Users
{
    public class UserRepository : BaseRepository<User, Email>, IUserRepository
    {
        public UserRepository(RobDroneGoAuthContext dbContext) : base(dbContext.Users)
        {

        }
    }
}