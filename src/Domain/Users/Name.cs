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
            return new Name(name);
        }

        public string NameString
        {
            get;
        }
    }
}