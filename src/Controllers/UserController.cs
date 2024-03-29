using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RobDroneGoAuth.Dto.Users;
using RobDroneGoAuth.Services.Users;
using System.IdentityModel.Tokens.Jwt;
using RobDroneGoAuth.Controllers.User;


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

        [HttpPost("backoffice")]
        public async Task<ActionResult<UserDto>> CreateBackofficeUser([FromBody] CreateBackofficeUserDto dto)
        {
            string role = GetRoleFromToken();
            if(role != "Admin") 
                return Unauthorized();
            try
            {
                var user = await this._userService.CreateBackofficeUser(dto);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserInfo(string id){
            string role = GetRoleFromToken();
            if(role != "TaskManager" && role != "Admin" && role != "FleetManager" && role != "Utente" && role != "CampusManager") 
                return Unauthorized();

            try
            {
                var user = await this._userService.GetUserInfo(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUser(string id){
            string role = GetRoleFromToken();
            if(role != "TaskManager" && role != "Admin" && role != "FleetManager" && role != "Utente" && role != "CampusManager") 
                return Unauthorized();

            try
            {
                var user = await this._userService.DeleteUser(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("edit")]
        public async Task<ActionResult<UserDto>> UpdateUser(UserDto dto){
            string role = GetRoleFromToken();
            if(role != "TaskManager" && role != "Admin" && role != "FleetManager" && role != "Utente" && role != "CampusManager") 
                return Unauthorized();

            try
            {
                var user = await this._userService.UpdateUser(dto);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("listAllUtentes")]
        public async Task<ActionResult<List<UserDto>>> GetAllUtentes(){
            string role = GetRoleFromToken();
            if(role != "TaskManager")
                return Unauthorized();


            try
            {
                var users = await this._userService.GetAllUtentes();
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
    }


    public virtual string GetRoleFromToken()
    {
        string token = HttpContext.Request.Headers["Authorization"];
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token.Replace("Bearer ", ""));

        var roleClaim = jwtToken.Claims.First(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
        string role = roleClaim.Value;

        return role;
    }
}
}