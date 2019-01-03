using RIDGID.Common.Api.Core.Utilities;
using System.ComponentModel.DataAnnotations;

namespace RIDGID.Common.Api.Core.Attributes
{
    public class RidgidRequiredAttribute : RidgidValidationAttribute
    {
        /// <summary>
        /// Indicates whether a value of blank will be treated as valid or not. Default is true.
        /// </summary>
        public bool AllowEmptyStrings { get; set; }

        public RidgidRequiredAttribute(int errorId, bool allowEmptyStrings = true) : base(errorId)
        {
            AllowEmptyStrings = allowEmptyStrings;
        }

        public RidgidRequiredAttribute(int errorId, string customErrorMessage, bool allowEmptyStrings = true) : base(errorId, customErrorMessage)
        {
            AllowEmptyStrings = allowEmptyStrings;
        }

        public override bool IsValid(object value)
        {
            return new RequiredAttribute()
            {
                AllowEmptyStrings = AllowEmptyStrings
            }.IsValid(value);
        }

        public override string FormatErrorMessage(string fieldName)
        {
            var errorMessage = CustomErrorMessage ?? CreateErrorMessage(fieldName);
            return ModelStateCustomErrorMessage.Create(ErrorId, errorMessage);
        }

        private static string CreateErrorMessage(string fieldName)
        {
            return $"The '{FormatResponseMessage.GetCasing(fieldName)}' field is required.";
        }
    }
}