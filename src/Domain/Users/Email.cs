using System.Text.Json.Serialization;
using DDDSample1.Domain.Shared;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RobDroneGoAuth.Domain.Users
{
    public class Email : EntityId
    {
        protected Email(string email) : base(email)
        {
        }

        public static Email Create(string email)
        {
            if (email == null)
            {
                throw new BusinessRuleValidationException("Email can't be null.");
            }
            if (!email.Contains('@'))
            {
                throw new BusinessRuleValidationException("Email must contain '@'.");
            }
            if (!email.Contains("isep.ipp.pt"))
            {
                throw new BusinessRuleValidationException("Email must be from 'isep.ipp.pt' domain.");
            }
            return new Email(email);
        }
    }
}