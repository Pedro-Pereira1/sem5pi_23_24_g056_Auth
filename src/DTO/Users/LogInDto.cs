namespace RobDroneGoAuth.Dto.Users
{
    public class LogInDto
    {
        public string Email { get; }
        public string Password { get; }

        public LogInDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}