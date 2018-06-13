namespace RIDGID.Common.Api.TestingUtilities.FieldValidations
{
    public class RidgidStringLengthFieldValidation : RidgidFieldValidation
    {
        public int MinLength { get; set; }

        public int MaxLength { get; set; }

        public RidgidStringLengthFieldValidation()
        {
            ValidationType = RidgidValidationType.StringLengthAttribute;
        }
    }
}