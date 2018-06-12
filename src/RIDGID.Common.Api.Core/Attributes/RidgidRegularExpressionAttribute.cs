using System.ComponentModel.DataAnnotations;
using RIDGID.Common.Api.Core.Utilities;

namespace RIDGID.Common.Api.Core.Attributes
{
    public class RidgidRegularExpressionAttribute : RegularExpressionAttribute
    {
        public int ErrorId { get; set; }

        public string CustomErrorMessage { get; set; }

        public RidgidRegularExpressionAttribute(int errorId, string regex) : base(regex)
        {
            this.ErrorId = errorId;
        }

        public RidgidRegularExpressionAttribute(int errorId, string regex, string customErrorMessage) : base(regex)
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