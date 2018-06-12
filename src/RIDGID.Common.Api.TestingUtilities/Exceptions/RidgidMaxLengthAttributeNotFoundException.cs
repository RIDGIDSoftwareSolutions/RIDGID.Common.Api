using System;
using RIDGID.Common.Api.Core.Attributes;

namespace RIDGID.Common.Api.TestingUtilities.Exceptions
{
    public class RidgidMaxLengthAttributeNotFoundException : Exception
    {
        public override string Message { get; }

        public RidgidMaxLengthAttributeNotFoundException(string fieldName)
        {
            Message = CreateErrorMessage(fieldName);
        }

        private static string CreateErrorMessage(string fieldName)
        {
            return $"The {nameof(RidgidMaxLengthAttribute)} could not be found on the {fieldName} field.";
        }
    }
}