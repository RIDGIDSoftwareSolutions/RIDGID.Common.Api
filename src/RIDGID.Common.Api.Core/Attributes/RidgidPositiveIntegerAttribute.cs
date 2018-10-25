using System.ComponentModel.DataAnnotations;
using RIDGID.Common.Api.Core.Utilities;

namespace RIDGID.Common.Api.Core.Attributes
{
    public class RidgidPositiveIntegerAttribute : RidgidValidationAttribute
    {
        private const int MIN = 0;
        private const int MAX = int.MaxValue;

        public RidgidPositiveIntegerAttribute(int errorId) : base(errorId)
        {
        }

        public RidgidPositiveIntegerAttribute(int errorId, string customErrorMessage) :
            base(errorId, customErrorMessage)
        {
        }

        public override bool IsValid(object value)
        {
            return value == null || new RangeAttribute(MIN, MAX).IsValid(value);
        }

        public override string FormatErrorMessage(string fieldName)
        {
            var errorMessage = CustomErrorMessage ?? CreateErrorMessage(fieldName);
            return ModelStateCustomErrorMessage.Create(ErrorId, errorMessage);
        }

        private string CreateErrorMessage(string fieldName)
        {
            return $"The '{FormatResponseMessage.GetCasing(fieldName)}' field must be an integer value between '{MIN}' and '{MAX}'.";
        }
    }
}
