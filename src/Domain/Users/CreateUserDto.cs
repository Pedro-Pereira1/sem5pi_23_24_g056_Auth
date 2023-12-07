namespace RobDroneGoAuth.Domain.Users
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int TaxPayerNumber { get; set; }
        public string Password { get; set; }

        public CreateUserDto(string name, string email, int phoneNumber, int taxPayerNumber, string password)
        {
            this.Name = name;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.TaxPayerNumber = taxPayerNumber;
            this.Password = password;
        }

    }
}