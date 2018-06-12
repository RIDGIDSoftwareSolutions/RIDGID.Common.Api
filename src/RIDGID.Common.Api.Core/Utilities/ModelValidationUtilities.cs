using System.Collections.Generic;
using System.Linq;
using RIDGID.Common.Api.Core.Exceptions;
using RIDGID.Common.Api.Core.Objects;

namespace RIDGID.Common.Api.Core.Utilities
{
    public class ModelValidationUtilities
    {
        private const char Separator = '|';

        public static ErrorMessage ParseModelStateErrorMessage(string modelStateErrorMessage)
        {
            if (!modelStateErrorMessage.Contains(Separator))
            {
                throw new InvalidModelStateErrorMessageException();
            }
            var errorId = int.Parse(modelStateErrorMessage.Split(Separator)[0]);
            var debugErrorMessage = modelStateErrorMessage.Split(Separator)[1];
            return new ErrorMessage
            {
                ErrorId = errorId,
                DebugErrorMessage = debugErrorMessage
            };
        }

        public static List<ErrorMessage> ParseModelStateErrorMessages(List<string> modelStateErrorMessages)
        {
            return modelStateErrorMessages.Select(ParseModelStateErrorMessage).ToList();
        }

        public static string CreateSpecialModelValidationMessage(int errorId, string baseMessage)
        {
            var str = errorId.ToString() + Separator + baseMessage;
            return str;
        }

        public static string CreateRequiredMessage(string fieldName)
        {
            return "The " + fieldName + " field is required.";
        }
    }
}