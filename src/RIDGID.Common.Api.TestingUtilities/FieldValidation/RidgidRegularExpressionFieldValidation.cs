namespace RIDGID.Common.Api.TestingUtilities.FieldValidation
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