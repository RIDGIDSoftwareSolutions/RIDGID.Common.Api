using System;
using RIDGID.Common.Api.Core.Attributes;

namespace RIDGID.Common.Api.TestingUtilities.Exceptions
{
    public class RidgidMinLengthAttributeNotFoundException : Exception
    {
        public override string Message { get; }

        public RidgidMinLengthAttributeNotFoundException(string fieldName)
        {
            Message = CreateErrorMessage(fieldName);
        }

        private static string CreateErrorMessage(string fieldName)
        {
            return $"The {nameof(RidgidMinLengthAttribute)} could not be found on the {fieldName} field.";
        }
    }
}