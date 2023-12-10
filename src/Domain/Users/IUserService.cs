using RobDroneGoAuth.Domain.Users;

namespace RobDroneGoAuth.Infrastructure.Users
{
    public interface IUserService
    {
        Task<UserDto> RegisterUser(CreateUserDto dto);
        Task<UserSessionDto> LogIn(LogInDto dto);
    }
}