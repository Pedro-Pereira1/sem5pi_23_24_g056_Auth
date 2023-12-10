using System.Text.RegularExpressions;
using DDDSample1.Domain.Shared;

namespace RobDroneGoAuth.Domain.Users
{
    public class Password : IValueObject
    {
        protected Password()
        {
        }
        protected Password(String password)
        {
            PasswordString = password;
        }

        public static Password Create(String password)
        {
            Regex uppercase = new Regex("[A-Z]");
            Regex lowercase = new Regex("[a-z]");
            Regex number = new Regex("[0-9]");
            Regex special = new Regex("[!@#$%^&*()_+]");

            if (password.Length < 10)
            {
                throw new BusinessRuleValidationException("Password must contain at least 10 characters");
            }
            if(uppercase.Matches(password).Count < 1)
            {
                throw new BusinessRuleValidationException("Password must contain at least one uppercase letter");
            }
            if (lowercase.Matches(password).Count < 1)
            {
                throw new BusinessRuleValidationException("Password must contain at least one lowercase letter");
            }
            if (number.Matches(password).Count < 1)
            {
                throw new BusinessRuleValidationException("Password must contain at least one number");
            }
            if (special.Matches(password).Count < 1)
            {
                throw new BusinessRuleValidationException("Password must contain at least one special character");
            }
            return new Password(password);
        }

        public String PasswordString
        {
            get;
        }

        public bool Equals(Password other)
        {
            return this.PasswordString.Equals(other.PasswordString);
        }
        public bool Equals(string other)
        {
            return this.PasswordString.Equals(other);
        }
    }
}