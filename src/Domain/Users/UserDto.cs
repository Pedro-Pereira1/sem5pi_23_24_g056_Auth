namespace RobDroneGoAuth.Domain.Users
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int TaxPayerNumber { get; set; }

        public UserDto(string name, string email, int phoneNumber, int taxPayerNumber)
        {
            this.Name = name;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.TaxPayerNumber = taxPayerNumber;
        }

    }
}