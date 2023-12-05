using DDDSample1.Domain.Shared;

namespace RobDroneGoAuth.Domain.Users
{
    public interface IUserRepository : IRepository<User, Email>
    {

    }
}