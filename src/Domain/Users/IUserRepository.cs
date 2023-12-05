using DDDSample1.Domain.Shared;

namespace Users.Repository
{
    public interface IUserRepository : IRepository<User, Email>
    {

    }
}