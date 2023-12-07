using System.Text.RegularExpressions;
using DDDSample1.Domain.Shared;

namespace RobDroneGoAuth.Domain.Users
{
    public class Name : IValueObject
    {
        protected Name()
        {
        }
        protected Name(string name)
        {
            NameString = name;
        }

        public static Name Create(string name)
        {
            Regex rx = new Regex(@"^[a-zA-Z ]+$");

            if (name.Length < 1)
            {
                throw new BusinessRuleValidationException("Name must contain at least 1 character");
            }
            if (!rx.IsMatch(name))
            {
                throw new BusinessRuleValidationException("Name must contain only letters");
            }

            return new Name(name);
        }

        public string NameString
        {
            get;
        }
    }
}