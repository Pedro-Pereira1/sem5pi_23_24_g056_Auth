using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RobDroneGoAuth.Controllers.User;
using RobDroneGoAuth.Dto.Users;
using RobDroneGoAuth.Services.Users;

namespace UserControllerTest;

[TestClass]
public class UserControllerTest
{


    [TestMethod]
    public async Task Check_Invalid_Values_For_User_Creation()
    {
        Mock<IUserService> userService = new Mock<IUserService>();
        userService.Setup(x => x.RegisterUser(It.IsAny<CreateUserDto>()))
            .ThrowsAsync(new BusinessRuleValidationException("Email must contain '@'."));

        UserController userController = new UserController(userService.Object);

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
        string name = "Jose Gouveia";
        string email = "1211089@isep.ipp.pt";
        string phoneNumber = "930597721";
        string taxPayerNumber = "290088763";
        string password = "1211089aA!";
        string role = "utente";

        CreateUserDto dto = new CreateUserDto(name, email, phoneNumber, taxPayerNumber, password);
        UserDto userDto = new UserDto(name, email, phoneNumber, taxPayerNumber, role);

        Mock<IUserService> userService = new Mock<IUserService>();
        userService.Setup(x => x.RegisterUser(It.IsAny<CreateUserDto>()))
            .ReturnsAsync(userDto);

        UserController userController = new UserController(userService.Object);

        var result = await userController.RegisterUser(dto);

        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        Assert.AreEqual(((OkObjectResult)result.Result).Value, userDto);
    }

    [TestMethod]
    public async Task Check_Invalid_Values_For_BackofficeUser_Creation()
    {
        Mock<IUserService> userService = new Mock<IUserService>();
        userService.Setup(x => x.CreateBackofficeUser(It.IsAny<CreateBackofficeUserDto>()))
            .ThrowsAsync(new BusinessRuleValidationException("Email must contain '@'."));

        UserController userController = new UserController(userService.Object);

        string name = "Jose Gouveia";
        string email = "1211089isep.ipp.pt";
        string phoneNumber = "930597721";
        string password = "1211089aA!";
        int role = 4;

        CreateBackofficeUserDto dto = new CreateBackofficeUserDto(name, email, phoneNumber, password, role);
        var result = await userController.CreateBackofficeUser(dto);

        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task Check_Valid_Role_For_BackofficeUser_Creation()
    {
        string name = "Jose Gouveia";
        string email = "1211089@isep.ipp.pt";
        string phoneNumber = "930597721";
        string password = "1211089aA!";
        int role = 0;

        CreateBackofficeUserDto dto = new CreateBackofficeUserDto(name, email, phoneNumber, password, role);
        UserDto userDto = new UserDto(name, email, phoneNumber, "999999999", "Admin");

        Mock<IUserService> userService = new Mock<IUserService>();
        userService.Setup(x => x.CreateBackofficeUser(It.IsAny<CreateBackofficeUserDto>()))
            .ReturnsAsync(userDto);

        UserController userController = new UserController(userService.Object);

        var result = await userController.CreateBackofficeUser(dto);

        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        Assert.AreEqual(userDto, ((OkObjectResult)result.Result).Value);
    }
}