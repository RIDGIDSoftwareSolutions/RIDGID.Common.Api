using System;
using RIDGID.Common.Api.Core.Attributes;

namespace RIDGID.Common.Api.TestingUtilities.Exceptions
{
    public class RidgidIso8601DateTimeAttributeNotFoundException : Exception
    {
        public override string Message { get; }

        public RidgidIso8601DateTimeAttributeNotFoundException(string fieldName)
        {
            Message = CreateErrorMessage(fieldName);
        }

        private static string CreateErrorMessage(string fieldName)
        {
            return $"The {nameof(RidgidIso8601DateTimeAttribute)} could not be found on the {fieldName} field.";
        }
    }
}
