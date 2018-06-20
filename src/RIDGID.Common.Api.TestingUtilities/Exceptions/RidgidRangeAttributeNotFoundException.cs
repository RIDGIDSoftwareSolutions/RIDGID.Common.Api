using RIDGID.Common.Api.Core.Attributes;
using System;

namespace RIDGID.Common.Api.TestingUtilities.Exceptions
{
    public class RidgidRangeAttributeNotFoundException : Exception
    {
        public override string Message { get; }

        public RidgidRangeAttributeNotFoundException(string fieldName)
        {
            Message = CreateErrorMessage(fieldName);
        }

        private static string CreateErrorMessage(string fieldName)
        {
            return $"The {nameof(RidgidRangeAttribute)} could not be found on the {fieldName} field.";
        }
    }
}