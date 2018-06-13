using RIDGID.Common.Api.Core.Utilities;
using System.ComponentModel.DataAnnotations;

namespace RIDGID.Common.Api.Core.Attributes
{
    public class RidgidRegularExpressionAttribute : RidgidValidationAttribute
    {
        public string Regex { get; set; }


        public RidgidRegularExpressionAttribute(int errorId, string regex) : base(errorId)
        {
            Regex = regex;
        }

        public RidgidRegularExpressionAttribute(int errorId, string regex, string customErrorMessage) : base(errorId,
            customErrorMessage)
        {
            Regex = regex;
        }

        public override bool IsValid(object value)
        {
            return new RegularExpressionAttribute(Regex).IsValid(value);
        }

        public override string FormatErrorMessage(string fieldName)
        {
            var errorMessage =
                CustomErrorMessage ?? new RegularExpressionAttribute(Regex).FormatErrorMessage(fieldName);
            return ModelStateCustomErrorMessage.Create(ErrorId, errorMessage);
        }
    }
}