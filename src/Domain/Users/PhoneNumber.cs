using DDDSample1.Domain.Shared;

namespace RobDroneGoAuth.Domain.Users
{
    public class PhoneNumber : IValueObject
    {
        private PhoneNumber(int phoneNumber)
        {
            Number = phoneNumber;
        }

        public static PhoneNumber Create(int phoneNumber)
        {
            if (phoneNumber < 0 || phoneNumber > 999999999)
            {
                throw new BusinessRuleValidationException("PhoneNumber must contain 9 digits");
            }
            return new PhoneNumber(phoneNumber);
        }

        public int Number
        {
            get;
        }
    }
}