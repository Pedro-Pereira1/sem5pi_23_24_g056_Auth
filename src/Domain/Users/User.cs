using DDDSample1.Domain.Shared;

namespace RobDroneGoAuth.Domain.Users
{

    public class User : IAggregateRoot
    {

        protected User()
        {
        }
        protected User(Name name, Email email, TaxpayerNumber taxPayerNumber, PhoneNumber phoneNumber, Password password)
        {
            Name = name;
            Email = email;
            TaxPayerNumber = taxPayerNumber;
            PhoneNumber = phoneNumber;
            Password = password;
        }
        
        public Email Email { get; }
        public Name Name { get; }
        public TaxpayerNumber TaxPayerNumber { get; }
        public PhoneNumber PhoneNumber { get; }
        public Password Password { get; }

        public static User Create(String name, String email, int taxPayerNumber, int phoneNumber, string password)
        {
            return new User(Name.Create(name),
                Email.Create(email),
                TaxpayerNumber.Create(taxPayerNumber),
                PhoneNumber.Create(phoneNumber),
                Password.Create(password));
        }

    }
}