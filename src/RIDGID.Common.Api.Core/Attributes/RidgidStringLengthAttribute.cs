using System;
using System.ComponentModel.DataAnnotations;
using RIDGID.Common.Api.Core.Utilities;

namespace RIDGID.Common.Api.Core.Attributes
{
    public class RidgidStringLengthAttribute : ValidationAttribute
    {
        private static string GenerateMessage(int min, int max, string fieldname)
        {
            return $"The '{fieldname}' string must be between {min} and {max} characters long.";
        }

        public int ErrorId { get; set; }

        public string CustomErrorMessage { get; set; }

        public int MininumLength { get; set; }

        public int MaximumLength { get; set; }

        public RidgidStringLengthAttribute(int errorId, int mininum, int maximum)
        {
            this.ErrorId = errorId;
            this.MininumLength = mininum;
            this.MaximumLength = maximum;
        }

        public RidgidStringLengthAttribute(int errorId, int mininum, int maximum, string customErrorMessage)
        {
            this.ErrorId = errorId;
            this.CustomErrorMessage = customErrorMessage;
            this.MininumLength = mininum;
            this.MaximumLength = maximum;
        }

        public override bool IsValid(object value)
        {
            var str = Convert.ToString(value);
            var result = str == "" || str.Length >= this.MininumLength && str.Length <= this.MaximumLength;
            return result;
        }

        public override string FormatErrorMessage(string fieldName)
        {
            var errorMessage = CustomErrorMessage ?? GenerateMessage(MininumLength, MaximumLength, fieldName);
            return ModelStateCustomErrorMessage.Create(ErrorId, errorMessage);
        }
    }
}