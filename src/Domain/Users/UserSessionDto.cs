namespace RobDroneGoAuth.Domain.Users
{
    public class UserSessionDto
    {
        public string Token { get; }
        public UserSessionDto(string token)
        {
            Token = token;
        }
    }

}