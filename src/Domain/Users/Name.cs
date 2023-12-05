using System.Text.RegularExpressions;
using DDDSample1.Domain.Shared;

namespace RoDroneGo.Domain.Users
{
    public class Name : IValueObject
    {
        private Name(String name)
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