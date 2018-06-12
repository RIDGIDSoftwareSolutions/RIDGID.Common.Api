namespace RIDGID.Common.Api.TestingUtilities.FieldValidation
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