using System;

namespace RIDGID.Common.Api.Core.Exceptions
{
    public class InvalidModelStateErrorMessageException : Exception
    {
        private const string MESSAGE =
            "The modelstate error message was supposed to follow the format of \"errorId|errorMessage\" but was \"errorMessage\"";

        public InvalidModelStateErrorMessageException() : base(MESSAGE)
        {
        }
    }
}