using DDDSample1.Domain.Shared;

namespace RobDroneGoAuth.Domain.Users
{

    public class User : Entity<Email>, IAggregateRoot
    {
        public Name Name { get; }
        public TaxPayerNumber TaxPayerNumber { get; }
        public PhoneNumber PhoneNumber { get; }
        public Password Password { get; }
        public Role Role { get; }

        protected User()
        {
        }
        protected User(Name name, Email email, TaxPayerNumber taxPayerNumber, PhoneNumber phoneNumber, Password password, Role role)
        {
            this.Id = email;
            Name = name;
            TaxPayerNumber = taxPayerNumber;
            PhoneNumber = phoneNumber;
            Password = password;
            Role = role;
        }

        public static User Create(string name, string email, int taxPayerNumber, int phoneNumber, string password, string role)
        {
            return new User(Name.Create(name),
                Email.Create(email),
                TaxPayerNumber.Create(taxPayerNumber),
                PhoneNumber.Create(phoneNumber),
                Password.Create(password),
                Role.Create(role));
        }

    }
}