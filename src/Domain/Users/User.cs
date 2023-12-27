using DDDSample1.Domain.Shared;

namespace RobDroneGoAuth.Domain.Users
{

    public class User : Entity<Email>, IAggregateRoot
    {
        public Name Name { get;private set; }
        public TaxPayerNumber TaxPayerNumber { get; private set;}
        public PhoneNumber PhoneNumber { get; private set;}
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

        public static User Create(string name, string email, string taxPayerNumber, string phoneNumber, string password, string role)
        {
            return new User(Name.Create(name),
                Email.Create(email),
                TaxPayerNumber.Create(taxPayerNumber),
                PhoneNumber.Create(phoneNumber),
                Password.Create(password),
                Role.Create(role));
        }

        public User UpdateName(Name name)
        {
            Name = name;
            return this;
        }

        public User UpdateTaxPayerNumber(TaxPayerNumber taxPayerNumber)
        {
            TaxPayerNumber = taxPayerNumber;
            return this;
        }

        public User UpdatePhoneNumber(PhoneNumber phoneNumber)
        {
            PhoneNumber = phoneNumber;
            return this;
        }
    }
}