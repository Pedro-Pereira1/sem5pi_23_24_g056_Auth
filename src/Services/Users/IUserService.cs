using RobDroneGoAuth.Domain.Users;
using RobDroneGoAuth.Dto.Users;

namespace RobDroneGoAuth.Services.Users
{
    public interface IUserService
    {
        Task<UserDto> RegisterUser(CreateUserDto dto);
        Task<UserSessionDto> LogIn(LogInDto dto);
        Task<UserDto> CreateBackofficeUser(CreateBackofficeUserDto dto);
        Task<UserDto> GetUserInfo(string id);
        Task<bool> DeleteUser(string id);
        Task<UserDto> UpdateUser(UserDto dto);
        Task<List<UserDto>> GetAllUtentes();
    }
}