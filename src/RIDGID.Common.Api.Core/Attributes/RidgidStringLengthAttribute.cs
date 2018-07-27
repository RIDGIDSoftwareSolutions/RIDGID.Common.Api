using RIDGID.Common.Api.Core.Utilities;
using System;

namespace RIDGID.Common.Api.Core.Attributes
{
    public class RidgidStringLengthAttribute : RidgidValidationAttribute
    {
        public int MininumLength { get; set; }

        public int MaximumLength { get; set; }

        public RidgidStringLengthAttribute(int errorId, int mininum, int maximum) : base(errorId)
        {
            MininumLength = mininum;
            MaximumLength = maximum;
        }

        public RidgidStringLengthAttribute(int errorId, int mininum, int maximum, string customErrorMessage) : base(
            errorId, customErrorMessage)
        {
            MininumLength = mininum;
            MaximumLength = maximum;
        }

        public override bool IsValid(object value)
        {
            var str = Convert.ToString(value);
            return str == "" || str.Length >= MininumLength && str.Length <= MaximumLength;
        }

        public override string FormatErrorMessage(string fieldName)
        {
            var errorMessage = CustomErrorMessage ?? CreateErrorMessage(fieldName);
            return ModelStateCustomErrorMessage.Create(ErrorId, errorMessage);
        }

        private string CreateErrorMessage(string fieldName)
        {
            return MininumLength == MaximumLength
                ? $"The '{FormatResponseMessage.GetCasing(fieldName)}' field must be '{MininumLength}' characters long."
                : $"The '{FormatResponseMessage.GetCasing(fieldName)}' field must be between '{MininumLength}' and '{MaximumLength}' characters long.";
        }
    }
}