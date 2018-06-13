namespace RIDGID.Common.Api.TestingUtilities.FieldValidations
{
    public class RidgidFieldValidation
    {
        public string FieldName { get; set; }

        public int ErrorId { get; set; }

        public string ErrorMessage { get; set; }

        internal RidgidValidationType ValidationType { get; set; }
    }
}