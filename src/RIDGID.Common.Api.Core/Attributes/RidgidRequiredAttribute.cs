using System.ComponentModel.DataAnnotations;
using RIDGID.Common.Api.Core.Utilities;

namespace RIDGID.Common.Api.Core.Attributes
{
    // The default error message from base class is "The FieldName is a required field."
    public class RidgidRequiredAttribute : RequiredAttribute
    {
        public int ErrorId { get; set; }

        public string CustomErrorMessage { get; set; }

        public RidgidRequiredAttribute(int errorId)
        {
            this.ErrorId = errorId;
        }

        public RidgidRequiredAttribute(int errorId, string customErrorMessage)
        {
            this.ErrorId = errorId;
            this.CustomErrorMessage = customErrorMessage;
        }

        public override string FormatErrorMessage(string fieldName)
        {
            var errorMessage = CustomErrorMessage ?? base.FormatErrorMessage(fieldName);
            return ModelValidationUtilities.CreateSpecialModelValidationMessage(ErrorId, errorMessage);
        }
    }
}