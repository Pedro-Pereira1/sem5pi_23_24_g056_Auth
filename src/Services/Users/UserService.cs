using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DDDSample1.Domain.Shared;
using Microsoft.IdentityModel.Tokens;
using RobDroneGoAuth.Domain.Users;
using RobDroneGoAuth.Dto.Users;

namespace RobDroneGoAuth.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _defaultRole = "Utente";

        public UserService(ILogger<UserService> logger, IUnitOfWork unitOfWork,
         IUserRepository userRepository, IConfiguration configuration)
        {
            this._logger = logger;
            this._unitOfWork = unitOfWork;
            this._userRepository = userRepository;
            this._configuration = configuration;
        }

        public async Task<UserDto> RegisterUser(CreateUserDto dto)
        {
            try
            {
                _logger.LogInformation("UserService: Registering user\n\n");

                var email = Email.Create(dto.Email);
                var userInDb = await this._userRepository.GetByIdAsync(email);
                if (userInDb != null)
                {
                    throw new BusinessRuleValidationException("Email already in use");
                }

                var user = User.Create(dto.Name, dto.Email, dto.TaxPayerNumber, dto.PhoneNumber, dto.Password, _defaultRole);
                await this._userRepository.AddAsync(user);
                await this._unitOfWork.CommitAsync();
                return new UserDto(user.Name.NameString, user.Id.Value, user.PhoneNumber.Number, user.TaxPayerNumber.Number, user.Role.Value);
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

                return new UserSessionDto(CreateToken(user));
            }
            catch (BusinessRuleValidationException e)
            {
                _logger.LogWarning("UserService: Error has occurred while logging in user: " + e.Message + "\n\n");
                throw new BusinessRuleValidationException(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("UserService: Error has occurred while logging in user: " + e.StackTrace + "\n\n");
                throw new Exception(e.Message);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.Value),
                new Claim(ClaimTypes.Role, user.Role.Value)
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
            var credentials = new SigningCredentials(key, "HS256");
            var jwt = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(jwt);
        }

        public async Task<UserDto> CreateBackofficeUser(CreateBackofficeUserDto dto)
        {
            try
            {
                _logger.LogInformation("UserService: Creating backoffice user\n\n");

                var email = Email.Create(dto.Email);
                var userInDb = await this._userRepository.GetByIdAsync(email);
                if (userInDb != null)
                {
                    throw new BusinessRuleValidationException("Email already in use");
                }

                var user = User.Create(dto.Name, dto.Email, "999999999", dto.PhoneNumber, dto.Password, dto.Role);
                await this._userRepository.AddAsync(user);
                await this._unitOfWork.CommitAsync();
                return new UserDto(user.Name.NameString, user.Id.Value, user.PhoneNumber.Number, user.TaxPayerNumber.Number, user.Role.Value);
            }
            catch (BusinessRuleValidationException e)
            {
                _logger.LogWarning("UserService: Error has occurred while creating backoffice user: " + e.Message + "\n\n");
                throw new BusinessRuleValidationException(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("UserService: Error has occurred while creating backoffice user: " + e.Message + "\n\n");
                throw new Exception(e.Message);
            }
        }

        public async Task<UserDto> GetUserInfo(string id){
            try
            {
                _logger.LogInformation("UserService: Getting current user\n\n");

                var email = Email.Create(id);
                var user = await this._userRepository.GetByIdAsync(email);
                if (user == null)
                {
                    throw new BusinessRuleValidationException("User not found");
                }
                return new UserDto(user.Name.NameString, user.Id.Value, user.PhoneNumber.Number, user.TaxPayerNumber.Number, user.Role.Value);
            }
            catch (BusinessRuleValidationException e)
            {
                _logger.LogWarning("UserService: Error has occurred while getting current user: " + e.Message + "\n\n");
                throw new BusinessRuleValidationException(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("UserService: Error has occurred while getting current user: " + e.Message + "\n\n");
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteUser(string id){
            try
            {
                _logger.LogInformation("UserService: Getting current user\n\n");

                var email = Email.Create(id);
                var user = await this._userRepository.GetByIdAsync(email);
                if (user == null)
                {
                    throw new BusinessRuleValidationException("User not found");
                } 
                this._userRepository.Remove(user);
                await this._unitOfWork.CommitAsync();
                return true;
            }
            catch (BusinessRuleValidationException e)
            {
                _logger.LogWarning("UserService: Error has occurred while getting current user: " + e.Message + "\n\n");
                throw new BusinessRuleValidationException(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("UserService: Error has occurred while getting current user: " + e.Message + "\n\n");
                throw new Exception(e.Message);
            }
        }

        public async Task<UserDto> UpdateUser(UserDto dto){
            try
            {
                _logger.LogInformation("UserService: Getting current user\n\n");

                var email = Email.Create(dto.Email);
                var user = await this._userRepository.GetByIdAsync(email);
                if (user == null)
                {
                    throw new BusinessRuleValidationException("User not found");
                } 
                if(dto.Name != null){
                    user.UpdateName(Name.Create(dto.Name));
                }
                if(dto.PhoneNumber != null){
                    user.UpdatePhoneNumber(PhoneNumber.Create(dto.PhoneNumber));
                }
                if(dto.TaxPayerNumber != null){
                    user.UpdateTaxPayerNumber(TaxPayerNumber.Create(dto.TaxPayerNumber));
                }
                await this._unitOfWork.CommitAsync();
                return new UserDto(user.Name.NameString, user.Id.Value, user.PhoneNumber.Number, user.TaxPayerNumber.Number, user.Role.Value);
            }
            catch (BusinessRuleValidationException e)
            {
                _logger.LogWarning("UserService: Error has occurred while getting current user: " + e.Message + "\n\n");
                throw new BusinessRuleValidationException(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("UserService: Error has occurred while getting current user: " + e.Message + "\n\n");
                throw new Exception(e.Message);
            }
        }

        public async Task<List<UserDto>> GetAllUtentes()
        {
        try
        {
            _logger.LogInformation("UserService: Getting all users\n\n");

            var users = await this._userRepository.GetAllUtentes();
            if (users == null || users.Count == 0)
            {
                throw new BusinessRuleValidationException("Users not found");
            } 

            List<UserDto> userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                if(user.Role.Value == "Utente")
                userDtos.Add(new UserDto(user.Name.NameString, user.Id.Value, user.PhoneNumber.Number, user.TaxPayerNumber.Number, user.Role.Value));
            }
            return userDtos;
        }
        catch (BusinessRuleValidationException e)
        {
            _logger.LogWarning("UserService: Error has occurred while getting all users: " + e.Message + "\n\n");
            throw new BusinessRuleValidationException(e.Message);
        }
    }   

}

    
}