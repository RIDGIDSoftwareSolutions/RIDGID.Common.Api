using System.ComponentModel.DataAnnotations;

namespace RIDGID.Common.Api.Core.Attributes
{
    public class RidgidValidationAttribute : ValidationAttribute
    {
        public int ErrorId { get; set; }

        public string CustomErrorMessage { get; set; }

        public RidgidValidationAttribute(int errorId)
        {
            ErrorId = errorId;
        }

        public RidgidValidationAttribute(int errorId, string customErrorMessage)
        {
            ErrorId = errorId;
            CustomErrorMessage = customErrorMessage;
        }
    }
}