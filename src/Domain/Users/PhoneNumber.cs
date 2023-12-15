using DDDSample1.Domain.Shared;

namespace RobDroneGoAuth.Domain.Users
{
    public class PhoneNumber : IValueObject
    {
        protected PhoneNumber()
        {
        }
        protected PhoneNumber(string phoneNumber)
        {
            Number = phoneNumber;
        }

        public static PhoneNumber Create(string phoneNumber)
        {
            if (phoneNumber.Length != 9)
            {
                throw new BusinessRuleValidationException("PhoneNumber must contain 9 digits");
            }
            return new PhoneNumber(phoneNumber);
        }

        public string Number
        {
            get;
        }
    }
}