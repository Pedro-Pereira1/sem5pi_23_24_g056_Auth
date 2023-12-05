using DDDSample1.Domain.Shared;

namespace RobDroneGoAuth.Domain.Users
{
    public class Password : IValueObject
    {
        private Password(String password)
        {
            PasswordString = password;
        }

        public static Password Create(String password)
        {
            if (password.Length < 10)
            {
                throw new BusinessRuleValidationException("Password must contain at least 10 characters");
            }
            return new Password(password);
        }

        public String PasswordString
        {
            get;
        }
    }
}