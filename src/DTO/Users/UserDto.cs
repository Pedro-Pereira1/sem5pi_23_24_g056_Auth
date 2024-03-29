namespace RobDroneGoAuth.Dto.Users
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TaxPayerNumber { get; set; }
        public string Role { get; set; }

        public UserDto(string name, string email, string phoneNumber, string taxPayerNumber, string role)
        {
            this.Name = name;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.TaxPayerNumber = taxPayerNumber;
            this.Role = role;
        }

    }
}