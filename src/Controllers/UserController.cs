using Microsoft.AspNetCore.Mvc;
using RobDroneGoAuth.Dto.Users;
using RobDroneGoAuth.Services.Users;

namespace RobDroneGoAuth.Controllers.User
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserSessionDto>> LogIn([FromBody] LogInDto dto)
        {
            try
            {
                var userSession = await this._userService.LogIn(dto);
                return Ok(userSession);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> RegisterUser([FromBody] CreateUserDto dto)
        {
            try
            {
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