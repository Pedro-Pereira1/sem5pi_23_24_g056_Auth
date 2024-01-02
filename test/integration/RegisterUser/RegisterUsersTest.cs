using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using RobDroneGoAuth.Controllers.User;
using RobDroneGoAuth.Domain.Users;
using RobDroneGoAuth.Dto.Users;
using RobDroneGoAuth.Services.Users;

namespace RegisterUser;

[TestClass]
public class RegisterUsersTest
{
    [TestMethod]
    public async Task Check_Invalid_Values_For_User_Creation()
    {
        var mockController = new Mock<UserController>();
        mockController.Setup(c => c.GetRoleFromToken()).Returns("Admin");
        Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
        Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
        Mock<ILogger<UserService>> logger = new Mock<ILogger<UserService>>();
        Mock<IConfiguration> configuration = new Mock<IConfiguration>();

        userRepository.Setup(x => x.AddAsync(It.IsAny<User>()));
        unitOfWork.Setup(x => x.CommitAsync());

        IUserService userService = new UserService(logger.Object, unitOfWork.Object, userRepository.Object, configuration.Object);

        UserController userController = new UserController(userService);

        string name = "Jose Gouveia";
        string email = "1211089isep.ipp.pt";
        string phoneNumber = "930597721";
        string taxPayerNumber = "290088763";
        string password = "1211089aA!";

        CreateUserDto dto = new CreateUserDto(name, email, phoneNumber, taxPayerNumber, password);
        var result = await userController.RegisterUser(dto);

        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task Check_Valid_Values_For_User_Creation()
    {
        var mockController = new Mock<UserController>();
        mockController.Setup(c => c.GetRoleFromToken()).Returns("Admin");
        Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
        Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
        Mock<ILogger<UserService>> logger = new Mock<ILogger<UserService>>();
        Mock<IConfiguration> configuration = new Mock<IConfiguration>();
        userRepository.Setup(x => x.AddAsync(It.IsAny<User>()));
        unitOfWork.Setup(x => x.CommitAsync());

        IUserService userService = new UserService(logger.Object, unitOfWork.Object, userRepository.Object, configuration.Object);
        UserController userController = new UserController(userService);

        string name = "Jose Gouveia";
        string email = "1211089@isep.ipp.pt";
        string phoneNumber = "930597721";
        string taxPayerNumber = "290088763";
        string password = "1211089aA!";
        string role = "Utente";
        CreateUserDto dto = new CreateUserDto(name, email, phoneNumber, taxPayerNumber, password);
        UserDto userDto = new UserDto(name, email, phoneNumber, taxPayerNumber, role);

        var result = await userController.RegisterUser(dto);

        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        Assert.AreEqual(((OkObjectResult)result.Result).Value.ToString(), userDto.ToString());
    }

    /*[TestMethod]
    public async Task Check_Invalid_Role_For_BackofficeUser_Creation()
    {
        
        

        Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
        Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
        Mock<ILogger<UserService>> logger = new Mock<ILogger<UserService>>();
        Mock<IConfiguration> configuration = new Mock<IConfiguration>();

        userRepository.Setup(x => x.AddAsync(It.IsAny<User>()));
        unitOfWork.Setup(x => x.CommitAsync());

        IUserService userService = new UserService(logger.Object, unitOfWork.Object, userRepository.Object, configuration.Object);

        var mockController = new Mock<UserController>(userService);
        mockController.Setup(c => c.GetRoleFromToken()).Returns("Admin");

        string name = "Jose Gouveia";
        string email = "1211089isep.ipp.pt";
        string phoneNumber = "930597721";
        string password = "1211089aA!";
        string role = "Manager";

        CreateBackofficeUserDto dto = new CreateBackofficeUserDto(name, email, phoneNumber, password, role);
        var result = await  mockController.Object.CreateBackofficeUser(dto);

        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task Check_Valid_Role_For_BackofficeUser_Creation()
    {
        Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
        Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
        Mock<ILogger<UserService>> logger = new Mock<ILogger<UserService>>();
        Mock<IConfiguration> configuration = new Mock<IConfiguration>();
        userRepository.Setup(x => x.AddAsync(It.IsAny<User>()));
        unitOfWork.Setup(x => x.CommitAsync());

        IUserService userService = new UserService(logger.Object, unitOfWork.Object, userRepository.Object, configuration.Object);
        
        var mockController = new Mock<UserController>(userService);
        mockController.Setup(c => c.GetRoleFromToken()).Returns("Admin");

        string name = "Jose Gouveia";
        string email = "1211089@isep.ipp.pt";
        string phoneNumber = "930597721";
        string password = "1211089aA!";
        string role = "Admin";
        CreateBackofficeUserDto dto = new CreateBackofficeUserDto(name, email, phoneNumber, password, role);
        UserDto userDto = new UserDto(name, email, phoneNumber, "999999999", "Admin");

        var result = await mockController.Object.CreateBackofficeUser(dto);

        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        Assert.AreEqual(((OkObjectResult)result.Result).Value.ToString(), userDto.ToString());
    }*/
    
}
