using Microsoft.AspNetCore.Mvc;
using RobDroneGoAuth.Domain.Users;
using RobDroneGoAuth.Infrastructure.Users;

namespace RobDroneGoAuth.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            this._userService = userService;
        }


        [HttpPost("create")]
        public async Task<ActionResult<UserDto>> Create(CreateUserDto dto)
        {
            try
            {
                Console.WriteLine("Create User");
                var user = await this._userService.RegisterUser(dto);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}