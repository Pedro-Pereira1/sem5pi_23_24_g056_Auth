using DDDSample1.Domain.Shared;
using RobDroneGoAuth.Domain.Users;

namespace RobDroneGoAuth.Infrastructure.Users
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            this._logger = logger;
            this._unitOfWork = unitOfWork;
            this._userRepository = userRepository;
        }

        public async Task<UserDto> RegisterUser(CreateUserDto dto)
        {
            try
            {
                _logger.LogInformation("UserService: Registering user\n\n");

                var user = User.Create(dto.Name, dto.Email, dto.TaxPayerNumber, dto.PhoneNumber, dto.Password);
                await this._userRepository.AddAsync(user);
                await this._unitOfWork.CommitAsync();
                return new UserDto(user.Name.NameString, user.Id.Value, user.PhoneNumber.Number, user.TaxPayerNumber.Number);
            }
            catch (BusinessRuleValidationException e)
            {
                _logger.LogError("UserService: Error has occurred while registering user: " + e.Message);
                throw new BusinessRuleValidationException(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("UserService: Error has occurred while registering user: " + e.Message);
                throw new Exception(e.Message);
            }
        }
    }
}