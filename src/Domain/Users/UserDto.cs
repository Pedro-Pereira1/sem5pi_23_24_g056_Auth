namespace RobDroneGoAuth.Domain.Users
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TaxPayerNumber { get; set; }

        public UserDto(string name, string email, string phoneNumber, string taxPayerNumber)
        {
            this.Name = name;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.TaxPayerNumber = taxPayerNumber;
        }

    }
}