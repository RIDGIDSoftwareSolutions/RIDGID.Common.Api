namespace RIDGID.Common.Api.TestingUtilities.FieldValidation
{
    public class RidgidRequiredFieldValidation : RidgidFieldValidation
    {
        public RidgidRequiredFieldValidation()
        {
            ValidationType = RidgidValidationType.RequiredAttribute;
        }
    }
}