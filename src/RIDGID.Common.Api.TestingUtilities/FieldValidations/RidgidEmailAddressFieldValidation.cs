namespace RIDGID.Common.Api.TestingUtilities.FieldValidations
{
    public class RidgidEmailAddressFieldValidation : RidgidFieldValidation
    {
        public RidgidEmailAddressFieldValidation()
        {
            ValidationType = RidgidValidationType.EmailAddressAttribute;
        }
    }
}