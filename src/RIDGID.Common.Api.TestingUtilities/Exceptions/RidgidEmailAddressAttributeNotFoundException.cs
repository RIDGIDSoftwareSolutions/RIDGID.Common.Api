using System;
using RIDGID.Common.Api.Core.Attributes;

namespace RIDGID.Common.Api.TestingUtilities.Exceptions
{
    public class RidgidEmailAddressAttributeNotFoundException : Exception
    {
        public override string Message { get; }

        public RidgidEmailAddressAttributeNotFoundException(string fieldName)
        {
            Message = CreateErrorMessage(fieldName);
        }

        private static string CreateErrorMessage(string fieldName)
        {
            return $"The {nameof(RidgidEmailAddressAttribute)} could not be found on the {fieldName} field.";
        }
    }
}