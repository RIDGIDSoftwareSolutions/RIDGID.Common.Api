namespace RIDGID.Common.Api.TestingUtilities.FieldValidations
{
    public class RidgidMinLengthFieldValidation : RidgidFieldValidation
    {
        public int MinLength { get; set; }

        public RidgidMinLengthFieldValidation()
        {
            ValidationType = RidgidValidationType.MinLengthAttribute;
        }
    }
}