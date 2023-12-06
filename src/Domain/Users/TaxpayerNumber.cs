using DDDSample1.Domain.Shared;

namespace RobDroneGoAuth.Domain.Users
{
    public class TaxPayerNumber : IValueObject
    {
        protected TaxPayerNumber()
        {
        }
        protected TaxPayerNumber(int taxpayerNumber)
        {
            Number = taxpayerNumber;
        }

        public static TaxPayerNumber Create(int taxpayerNumber)
        {
            if (taxpayerNumber < 0 || taxpayerNumber > 999999999)
            {
                throw new BusinessRuleValidationException("TaxPayerNumber must contain 9 digits");
            }
            return new TaxPayerNumber(taxpayerNumber);
        }

        public int Number
        {
            get;
        }
    }
}