using System.Text.Json.Serialization;
using DDDSample1.Domain.Shared;

namespace RobDroneGoAuth.Domain.Users
{
    public class Email : EntityId
    {
        [JsonConstructor]
        protected Email(Guid guid) : base(guid)
        {
        }
        protected Email(String email) : base(email)
        {
        }

        public static Email Create(String email)
        {
            if (!email.EndsWith("@isep.ipp.pt"))
            {
                throw new BusinessRuleValidationException("Email must be an ISEP email");
            }
            return new Email(email);
        }

        protected override object createFromString(string text)
        {
            return Email.Create(text);
        }

        public override string AsString()
        {
            return Value;
        }
    }
}