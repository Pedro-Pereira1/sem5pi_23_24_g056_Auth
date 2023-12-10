using DDDSample1.Domain.Shared;
using RobDroneGoAuth.Domain.Users;

namespace RobDroneGoAuth.Infrastructure.Users
{
    public class UserService : IUserService
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

        public async Task<UserSessionDto> LogIn(LogInDto dto)
        {
            try
            {
                _logger.LogInformation("UserService: Logging in user\n\n");

                Email email = Email.Create(dto.Email);
                var user = await this._userRepository.GetByIdAsync(email);
                if (user == null)
                {
                    _logger.LogWarning("UserService: Error has occurred while logging in user: User not found\n\n");
                    throw new BusinessRuleValidationException("User not found");
                }
                if (!user.Password.Equals(dto.Password))
                {
                    _logger.LogWarning("UserService: Error has occurred while logging in user: Wrong password\n\n");
                    throw new BusinessRuleValidationException("Wrong password");
                }
                return new UserSessionDto(user.Id.Value);
            }
            catch (BusinessRuleValidationException e)
            {
                _logger.LogWarning("UserService: Error has occurred while logging in user: " + e.Message + "\n\n");
                throw new BusinessRuleValidationException(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("UserService: Error has occurred while logging in user: " + e.Message + "\n\n");
                throw new Exception(e.Message);
            }
        }
    }
}