namespace RIDGID.Common.Api.TestingUtilities.FieldValidation
{
    public class RidgidFieldValidation
    {
        public string FieldName { get; set; }

        public int ErrorId { get; set; }

        public string ErrorMessage { get; set; }

        internal RidgidValidationType ValidationType { get; set; }
    }
}