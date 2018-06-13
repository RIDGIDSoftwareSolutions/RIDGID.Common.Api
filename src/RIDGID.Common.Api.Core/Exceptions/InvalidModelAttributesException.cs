using System;

namespace RIDGID.Common.Api.Core.Exceptions
{
    public class InvalidModelAttributesException : Exception
    {
        private const string MESSAGE = "The model should have only RidgidValidationAttributes.";

        public InvalidModelAttributesException() : base(MESSAGE)
        {

        }
    }
}