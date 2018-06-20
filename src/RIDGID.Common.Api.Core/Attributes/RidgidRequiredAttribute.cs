using RIDGID.Common.Api.Core.Utilities;
using System.ComponentModel.DataAnnotations;

namespace RIDGID.Common.Api.Core.Attributes
{
    public class RidgidRequiredAttribute : RidgidValidationAttribute
    {
        public RidgidRequiredAttribute(int errorId) : base(errorId)
        {

        }

        public RidgidRequiredAttribute(int errorId, string customErrorMessage) : base(errorId, customErrorMessage)
        {

        }

        public override bool IsValid(object value)
        {
            return new RequiredAttribute().IsValid(value);
        }

        public override string FormatErrorMessage(string fieldName)
        {
            var errorMessage = CustomErrorMessage ?? CreateErrorMessage(fieldName);
            return ModelStateCustomErrorMessage.Create(ErrorId, errorMessage);
        }

        private static string CreateErrorMessage(string fieldName)
        {
            return $"The '{fieldName}' field is required.";
        }
    }
}