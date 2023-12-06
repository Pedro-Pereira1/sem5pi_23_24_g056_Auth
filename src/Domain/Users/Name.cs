using System.Text.RegularExpressions;
using DDDSample1.Domain.Shared;

namespace RobDroneGoAuth.Domain.Users
{
    public class Name : IValueObject
    {
        protected Name()
        {
        }
        protected Name(String name)
        {
            NameString = name;
        }

        public static Name Create(String name)
        {
            if(!Regex.Match(name, "/^[a-zA-Z]+$/i").Success)
            {
                throw new BusinessRuleValidationException("Name must contain only letters");
            }
            return new Name(name);
        }

        public String NameString
        {
            get;
        }
    }
}