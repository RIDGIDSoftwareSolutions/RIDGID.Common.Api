using RIDGID.Common.Api.Core.Utilities;
using System;

namespace RIDGID.Common.Api.Core.Attributes
{
    public class RidgidStringLengthAttribute : RidgidValidationAttribute
    {
        private string GenerateMessage(string fieldname)
        {
            return $"The '{fieldname}' string must be between {MininumLength} and {MaximumLength} characters long.";
        }

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
            var errorMessage = CustomErrorMessage ?? GenerateMessage(fieldName);
            return ModelStateCustomErrorMessage.Create(ErrorId, errorMessage);
        }
    }
}