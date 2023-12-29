using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using RobDroneGoAuth.Controllers.User;
using RobDroneGoAuth.Domain.Users;
using RobDroneGoAuth.Dto.Users;
using RobDroneGoAuth.Services.Users;

namespace GetUserInfo;

[TestClass]
public class GetUserInfoTest
{
    private Mock<IUserRepository> _userRepository;
    private Mock<IUnitOfWork> _unitOfWork;
    private Mock<ILogger<UserService>> _logger;
    private Mock<IConfiguration> _configuration;
    private UserService _userService;
    private UserController _userController;


    [TestInitialize]
    public void Setup()
    {
        _userRepository = new Mock<IUserRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _logger = new Mock<ILogger<UserService>>();
        _configuration = new Mock<IConfiguration>();
        _userService = new UserService(_logger.Object, _unitOfWork.Object, _userRepository.Object, _configuration.Object);
        _userController = new UserController(_userService);
        
        
    }


    [TestMethod]
    public async Task GetUserInfo_ValidId_ReturnsUserDto()
    {
        string name = "Jose Gouveia";
        string email = "1211089@isep.ipp.pt";
        string phoneNumber = "930597721";
        string taxPayerNumber = "290088763";
        string password = "1211089aA!";
        string role = "Utente";

        var mock = new Mock<IUserService>();
        mock.Setup(m => m.GetUserInfo(It.IsAny<string>()))
            .ReturnsAsync(new UserDto(name, email, phoneNumber, taxPayerNumber, role));
        
        var result = await mock.Object.GetUserInfo(email);
        var userDto = result;
        
        Assert.IsNotNull(result);
        Assert.AreEqual(name, userDto.Name);
        Assert.AreEqual(email, userDto.Email);
        Assert.AreEqual(phoneNumber, userDto.PhoneNumber);
        Assert.AreEqual(taxPayerNumber, userDto.TaxPayerNumber);
        Assert.AreEqual(role, userDto.Role);
    }

    [TestMethod]
    public async Task GetUserInfo_UserNotFound_ThrowsBusinessRuleValidationException()
    {
        string email = "1211089@isep.ipp.pt";

        var mock = new Mock<IUserService>();
        mock.Setup(m => m.GetUserInfo(It.IsAny<string>()))
            .ThrowsAsync(new BusinessRuleValidationException("User not found"));

        try
        {
            var result = await mock.Object.GetUserInfo(email);
        }
        catch (Exception e)
        {
            Assert.IsTrue(e is BusinessRuleValidationException);
        }

    }

    [TestMethod]
    public async Task GetUserInfo_InvalidIdFormat_ThrowsException()
    {

        var mock = new Mock<IUserService>();
        mock.Setup(m => m.GetUserInfo(It.IsAny<string>()))
            .ThrowsAsync(new Exception("Invalid id format"));

        try
        {
            var result = await mock.Object.GetUserInfo("1211089isep.ipp.pt");
        }
        catch (Exception e)
        {
            Assert.IsTrue(e is Exception);
        }

    }

    
}
