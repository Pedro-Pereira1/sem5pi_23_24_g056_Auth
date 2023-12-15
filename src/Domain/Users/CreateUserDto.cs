namespace RobDroneGoAuth.Domain.Users
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TaxPayerNumber { get; set; }
        public string Password { get; set; }

        public CreateUserDto(string name, string email, string phoneNumber, string taxPayerNumber, string password)
        {
            this.Name = name;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.TaxPayerNumber = taxPayerNumber;
            this.Password = password;
        }

    }
}