using DDDSample1.Domain.Shared;
using RobDroneGoAuth.Domain.Users;

namespace RobDroneGoAuth.Infrastructure.Users
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            this._unitOfWork = unitOfWork;
            this._userRepository = userRepository;
        }

        public async Task<UserDto> RegisterUser(CreateUserDto dto)
        {
            try
            {
                var user = User.Create(dto.Name, dto.Email, dto.TaxPayerNumber, dto.PhoneNumber, dto.Password);
                await this._userRepository.AddAsync(user);
                await this._unitOfWork.CommitAsync();
                return new UserDto(user.Name.NameString, user.Id.Value, user.PhoneNumber.Number, user.TaxPayerNumber.Number);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}