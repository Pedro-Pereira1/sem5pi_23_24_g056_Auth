namespace RobDroneGoAuth.Dto.Users
{
    public class CreateBackofficeUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }

        public CreateBackofficeUserDto(string name, string email, string phoneNumber, string password, int role)
        {
            this.Name = name;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Password = password;
            this.Role = role;
        }

    }
}