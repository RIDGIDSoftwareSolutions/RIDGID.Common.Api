namespace RIDGID.Common.Api.TestingUtilities.FieldValidation
{
    public class RidgidEmailAddressFieldValidation : RidgidFieldValidation
    {
        public RidgidEmailAddressFieldValidation()
        {
            ValidationType = RidgidValidationType.EmailAddressAttribute;
        }
    }
}