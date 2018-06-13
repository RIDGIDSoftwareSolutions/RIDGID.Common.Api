namespace RIDGID.Common.Api.TestingUtilities.FieldValidations
{
    public class RidgidRequiredFieldValidation : RidgidFieldValidation
    {
        public RidgidRequiredFieldValidation()
        {
            ValidationType = RidgidValidationType.RequiredAttribute;
        }
    }
}