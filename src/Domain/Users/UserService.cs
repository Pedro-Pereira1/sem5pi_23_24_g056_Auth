using DDDSample1.Domain.Shared;
using RobDroneGoAuth.Domain.Users;

namespace RobDroneGoAuth.Infrastructure.Users
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly string _defaultRole = "utente";

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

                var user = User.Create(dto.Name, dto.Email, dto.TaxPayerNumber, dto.PhoneNumber, dto.Password, _defaultRole);
                await this._userRepository.AddAsync(user);
                await this._unitOfWork.CommitAsync();
                return new UserDto(user.Name.NameString, user.Id.Value, user.PhoneNumber.Number, user.TaxPayerNumber.Number);
            }
            catch (BusinessRuleValidationException e)
            {
                _logger.LogWarning("UserService: Error has occurred while registering user: " + e.Message + "\n\n");
                throw new BusinessRuleValidationException(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("UserService: Error has occurred while registering user: " + e.Message + "\n\n");
                throw new Exception(e.Message);
            }
        }
    }
}