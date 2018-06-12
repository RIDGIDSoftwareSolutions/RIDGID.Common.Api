using System;
using RIDGID.Common.Api.Core.Attributes;

namespace RIDGID.Common.Api.TestingUtilities.Exceptions
{
    public class RidgidRequiredAttributeNotFoundException : Exception
    {
        public override string Message { get; }

        public RidgidRequiredAttributeNotFoundException(string fieldName)
        {
            Message = CreateErrorMessage(fieldName);
        }

        private static string CreateErrorMessage(string fieldName)
        {
            return $"The {nameof(RidgidRequiredAttribute)} could not be found on the {fieldName} field.";
        }
    }
}