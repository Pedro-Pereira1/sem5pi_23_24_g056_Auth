using RobDroneGoAuth.Dto.Users;

namespace RobDroneGoAuth.Services.Users
{
    public interface IUserService
    {
        Task<UserDto> RegisterUser(CreateUserDto dto);
        Task<UserSessionDto> LogIn(LogInDto dto);
    }
}