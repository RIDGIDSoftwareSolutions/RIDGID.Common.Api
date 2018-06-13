using System.ComponentModel.DataAnnotations;
using RIDGID.Common.Api.Core.Utilities;

namespace RIDGID.Common.Api.Core.Attributes
{
    public class RidgidMinLengthAttribute : MinLengthAttribute
    {
        public int ErrorId { get; set; }

        public string CustomErrorMessage { get; set; }


        public RidgidMinLengthAttribute(int errorId, int length) : base(length)
        {
            this.ErrorId = errorId;
        }

        public RidgidMinLengthAttribute(int errorId, int length, string customErrorMessage) : base(length)
        {
            this.ErrorId = errorId;
            this.CustomErrorMessage = customErrorMessage;
        }

        public override string FormatErrorMessage(string fieldName)
        {
            var errorMessage = CustomErrorMessage ?? base.FormatErrorMessage(fieldName);
            return ModelStateCustomErrorMessage.Create(ErrorId, errorMessage);
        }
    }
}