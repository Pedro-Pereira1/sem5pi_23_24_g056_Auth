using Moq;
using RobDroneGoAuth.Domain.Users;
using RobDroneGoAuth.Dto.Users;
using RobDroneGoAuth.Services.Users;
using DDDSample1.Domain.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace UserServiceTest;

[TestClass]
public class UserServiceTest
{

    private static Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
    private static Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
    private static Mock<ILogger<UserService>> _logger = new Mock<ILogger<UserService>>();
    private static Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
    private static UserService _userService = new UserService(_logger.Object, _unitOfWork.Object, _userRepository.Object, _configuration.Object);

    [TestMethod]
    public async Task Check_Invalid_Values_For_User_Creation()
    {
        string name = "Jose Gouveia";
        string email = "1211089isep.ipp.pt";
        string phoneNumber = "930597721";
        string taxPayerNumber = "290088763";
        string password = "1211089aA!";

        CreateUserDto dto = new CreateUserDto(name, email, phoneNumber, taxPayerNumber, password);
        try
        {
            var user = await _userService.RegisterUser(dto);
        }
        catch (Exception e)
        {
            Assert.IsTrue(e is BusinessRuleValidationException);
        }
    }

    [TestMethod]
    public async Task Check_Valid_Values_For_User_Creation()
    {
        string name = "Jose Gouveia";
        string email = "1211089@isep.ipp.pt";
        string phoneNumber = "930597721";
        string taxPayerNumber = "290088763";
        string password = "1211089aA!";

        CreateUserDto dto = new CreateUserDto(name, email, phoneNumber, taxPayerNumber, password);
        _userRepository.Setup(x => x.AddAsync(It.IsAny<User>()));
        _unitOfWork.Setup(x => x.CommitAsync());

        var user = await _userService.RegisterUser(dto);

        Assert.AreEqual(user.Name, name);
        Assert.AreEqual(user.Email, email);
        Assert.AreEqual(user.PhoneNumber, phoneNumber);
        Assert.AreEqual(user.TaxPayerNumber, taxPayerNumber);
    }

    [TestMethod]
    public async Task Check_Valid_Role_For_BackofficeUser_Creation()
    {
        string name = "Jose Gouveia";
        string email = "1211089@isep.ipp.pt";
        string phoneNumber = "930597721";
        string password = "1211089aA!";
        string role = "Admin";

        CreateBackofficeUserDto dto = new CreateBackofficeUserDto(name, email, phoneNumber, password, role);
        _userRepository.Setup(x => x.AddAsync(It.IsAny<User>()));
        _unitOfWork.Setup(x => x.CommitAsync());

        var user = await _userService.CreateBackofficeUser(dto);

        Assert.AreEqual(name, user.Name);
        Assert.AreEqual(email, user.Email);
        Assert.AreEqual(phoneNumber, user.PhoneNumber);
        Assert.AreEqual("Admin", user.Role);
    }

    [TestMethod]
    public async Task Check_Invalid_Role_For_BackofficeUser_Creation()
    {
        string name = "Jose Gouveia";
        string email = "1211089@isep.ipp.pt";
        string phoneNumber = "930597721";
        string password = "1211089aA!";
        string role = "Manager";

        CreateBackofficeUserDto dto = new CreateBackofficeUserDto(name, email, phoneNumber, password, role);

        try
        {
            var user = await _userService.CreateBackofficeUser(dto);
        }
        catch (Exception e)
        {
            Assert.IsTrue(e is BusinessRuleValidationException);
        }
    }
}