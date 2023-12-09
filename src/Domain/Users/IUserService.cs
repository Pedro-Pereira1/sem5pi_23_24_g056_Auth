using RobDroneGoAuth.Domain.Users;

namespace RobDroneGoAuth.Infrastructure.Users
{
    public interface IUserService
    {
        Task<UserDto> RegisterUser(CreateUserDto dto);
    }
}