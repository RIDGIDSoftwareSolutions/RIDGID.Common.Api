using System;
using RIDGID.Common.Api.Core.Attributes;

namespace RIDGID.Common.Api.TestingUtilities.Exceptions
{
    public class RidgidRegularExpressionAttributeNotFoundException : Exception
    {
        public override string Message { get; }

        public RidgidRegularExpressionAttributeNotFoundException(string fieldName)
        {
            Message = CreateErrorMessage(fieldName);
        }

        private static string CreateErrorMessage(string fieldName)
        {
            return $"The {nameof(RidgidRegularExpressionAttribute)} could not be found on the {fieldName} field.";
        }
    }
}