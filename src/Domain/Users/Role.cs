using DDDSample1.Domain.Shared;

namespace RobDroneGoAuth.Domain.Users
{
    public class Role : IValueObject
    {
        public static Role Create(string value)
        {
            return new Role(value);
        }

        private Role(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}