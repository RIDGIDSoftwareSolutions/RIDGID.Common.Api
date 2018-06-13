using System.ComponentModel.DataAnnotations;
using RIDGID.Common.Api.Core.Utilities;

namespace RIDGID.Common.Api.Core.Attributes
{
    public class RidgidEmailAddressAttribute : ValidationAttribute
    {
        public int ErrorId { get; set; }

        public string CustomErrorMessage { get; set; }

        public RidgidEmailAddressAttribute(int errorId)
        {
            this.ErrorId = errorId;
        }

        public RidgidEmailAddressAttribute(int errorId, string customErrorMessage)
        {
            this.ErrorId = errorId;
            this.CustomErrorMessage = customErrorMessage;
        }

        public override bool IsValid(object value)
        {
            var emailAddressAttributeClass = new EmailAddressAttribute();
            return emailAddressAttributeClass.IsValid(value);
        }

        public override string FormatErrorMessage(string fieldName)
        {
            var errorMessage = CustomErrorMessage ?? new EmailAddressAttribute().FormatErrorMessage(fieldName);
            return ModelStateCustomErrorMessage.Create(ErrorId, errorMessage);
        }
    }
}