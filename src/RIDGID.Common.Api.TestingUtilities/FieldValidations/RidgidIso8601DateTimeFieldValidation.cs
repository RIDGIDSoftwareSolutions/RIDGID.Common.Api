namespace RIDGID.Common.Api.TestingUtilities.FieldValidations
{
    public class RidgidIso8601DateTimeFieldValidation : RidgidFieldValidation
    {
        public RidgidIso8601DateTimeFieldValidation()
        {
            ValidationType = RidgidValidationType.Iso8601DateTimeAttribute;
        }
    }
}
