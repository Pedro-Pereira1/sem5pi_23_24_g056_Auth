using DDDSample1.Domain.Shared;

namespace RoDroneGo.Domain.Users
{
    public class Email : IValueObject
    {
        private Email(String email)
        {
            EmailAddress = email;
        }

        public static Email Create(String email)
        {
            if (!email.EndsWith("@isep.ipp.pt"))
            {
                throw new BusinessRuleValidationException("Email must be an ISEP email");
            }
            return new Email(email);
        }

        public String EmailAddress
        {
            get;
        }
    }
}