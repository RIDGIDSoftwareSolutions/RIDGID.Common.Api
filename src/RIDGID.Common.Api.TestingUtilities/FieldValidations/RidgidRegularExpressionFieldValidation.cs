namespace RIDGID.Common.Api.TestingUtilities.FieldValidations
{
    public class RidgidRegularExpressionFieldValidation : RidgidFieldValidation
    {
        public string Regex { get; set; }

        public RidgidRegularExpressionFieldValidation()
        {
            ValidationType = RidgidValidationType.RegularExpressionAttribute;
        }
    }
}