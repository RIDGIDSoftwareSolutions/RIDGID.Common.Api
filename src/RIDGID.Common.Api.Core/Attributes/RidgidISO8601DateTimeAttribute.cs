using System.ComponentModel.DataAnnotations;
using RIDGID.Common.Api.Core.Utilities;

namespace RIDGID.Common.Api.Core.Attributes
{
    public class RidgidIso8601DateTimeAttribute : RidgidValidationAttribute
    {
        public const string DateTimeRegex =
            "^(-?(?:[1-9][0-9]*)?[0-9]{4})-(1[0-2]|0[1-9])-(3[01]|0[1-9]|[12][0-9])T(2[0-3]|[01][0-9]):([0-5][0-9]):([0-5][0-9])(\\.[0-9]+)?(Z)?$";

        public RidgidIso8601DateTimeAttribute(int errorId) : base(errorId)
        {
        }

        public RidgidIso8601DateTimeAttribute(int errorId, string customErrorMessage) : base(
            errorId, customErrorMessage)
        {
        }

        public override bool IsValid(object value)
        {
            return value == null || new RegularExpressionAttribute(DateTimeRegex).IsValid(value);
        }

        public override string FormatErrorMessage(string fieldName)
        {
            var errorMessage = CustomErrorMessage ?? CreateErrorMessage(fieldName);
            return ModelStateCustomErrorMessage.Create(ErrorId, errorMessage);
        }

        private string CreateErrorMessage(string fieldName)
        {
            return $"The '{FormatResponseMessage.GetCasing(fieldName)}' must be in ISO-8601 format, (i.e. yyyy-mm-ddThh:mm:ss.ffffff).";
        }
    }
}
