using System.Text.Json.Serialization;
using DDDSample1.Domain.Shared;

namespace RobDroneGoAuth.Domain.Users
{
    public class Email : EntityId
    {
        protected Email(string email) : base(email)
        {
        }

        public static Email Create(string email)
        {
            return new Email(email);
        }
    }
}