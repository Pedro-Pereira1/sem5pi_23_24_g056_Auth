using DDDSample1.Domain.Shared;

namespace RoDroneGo.Domain.Users
{
    public class TaxpayerNumber : IValueObject
    {
        private TaxpayerNumber(int taxpayerNumber)
        {
            Number = taxpayerNumber;
        }

        public static TaxpayerNumber Create(int taxpayerNumber)
        {
            if (taxpayerNumber < 0 || taxpayerNumber > 999999999)
            {
                throw new BusinessRuleValidationException("TaxPayerNumber must contain 9 digits");
            }
            return new TaxpayerNumber(taxpayerNumber);
        }

        public int Number
        {
            get;
        }
    }
}