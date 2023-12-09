using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RobDroneGoAuth.Controllers.User;
using RobDroneGoAuth.Domain.Users;
using RobDroneGoAuth.Infrastructure.Users;

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
        int phoneNumber = 930597721;
        int taxPayerNumber = 290088763;
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
        int phoneNumber = 930597721;
        int taxPayerNumber = 290088763;
        string password = "1211089aA!";

        CreateUserDto dto = new CreateUserDto(name, email, phoneNumber, taxPayerNumber, password);
        UserDto userDto = new UserDto(name, email, phoneNumber, taxPayerNumber);

        Mock<IUserService> userService = new Mock<IUserService>();
        userService.Setup(x => x.RegisterUser(It.IsAny<CreateUserDto>()))
            .ReturnsAsync(userDto);

        UserController userController = new UserController(userService.Object);

        var result = await userController.RegisterUser(dto);

        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        Assert.AreEqual(((OkObjectResult)result.Result).Value, userDto);
    }
}