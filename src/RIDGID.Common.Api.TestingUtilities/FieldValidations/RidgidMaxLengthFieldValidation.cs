namespace RIDGID.Common.Api.TestingUtilities.FieldValidations
{
    public class RidgidMaxLengthFieldValidation : RidgidFieldValidation
    {
        public int MaxLength { get; set; }

        public RidgidMaxLengthFieldValidation()
        {
            ValidationType = RidgidValidationType.MaxLengthAttribute;
        }
    }
}