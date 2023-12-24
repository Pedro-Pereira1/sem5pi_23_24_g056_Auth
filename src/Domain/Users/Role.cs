using DDDSample1.Domain.Shared;

namespace RobDroneGoAuth.Domain.Users
{
    public class Role : IValueObject
    {
        public static Role Create(string value)
        {
            RoleType roleType;
            if (!Enum.TryParse(value, out roleType))
            {
                throw new BusinessRuleValidationException("Invalid role.");
            }
            return new Role(value);
        }

        protected Role(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}