using DDDSample1.Domain.Shared;

namespace RobDroneGoAuth.Domain.Users
{
    public class TaxPayerNumber : IValueObject
    {
        protected TaxPayerNumber()
        {
        }
        protected TaxPayerNumber(string taxpayerNumber)
        {
            Number = taxpayerNumber;
        }

        public static TaxPayerNumber Create(string taxpayerNumber)
        {
            if (taxpayerNumber.Length != 9)
            {
                throw new BusinessRuleValidationException("TaxPayerNumber must contain 9 digits");
            }
            return new TaxPayerNumber(taxpayerNumber);
        }

        public string Number
        {
            get;
        }
    }
}