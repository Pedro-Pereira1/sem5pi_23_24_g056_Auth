using DDDSample1.Domain.Shared;

namespace RobDroneGoAuth.Domain.Users
{

    public class User : IAggregateRoot
    {

        private User(Name name, Email email, TaxpayerNumber taxPayerNumber, PhoneNumber phoneNumber, Password password)
        {
            Name = name;
            Email = email;
            TaxPayerNumber = taxPayerNumber;
            PhoneNumber = phoneNumber;
            Password = password;
        }
        public Name Name { get; }
        public Email Email { get; }
        public TaxpayerNumber TaxPayerNumber { get; }
        public PhoneNumber PhoneNumber { get; }
        public Password Password { get; }

        public static User Create(String name, String email, int taxPayerNumber, int phoneNumber)
        {
            return new User(Name.Create(name),
                Email.Create(email),
                TaxpayerNumber.Create(taxPayerNumber),
                PhoneNumber.Create(phoneNumber),
                Password.Create("1234567890"));
        }

    }
}