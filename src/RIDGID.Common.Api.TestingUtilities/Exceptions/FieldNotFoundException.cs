using System;

namespace RIDGID.Common.Api.TestingUtilities.Exceptions
{
    public class FieldNotFoundException : Exception
    {
        public override string Message { get; }

        public FieldNotFoundException(string fieldName)
        {
            Message = CreateErrorMessage(fieldName);
        }

        private static string CreateErrorMessage(string fieldName)
        {
            return $"The FieldName '{fieldName}' could not be found on the specified model.";
        }
    }
}