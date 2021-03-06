﻿using System;
using RIDGID.Common.Api.Core.Attributes;

namespace RIDGID.Common.Api.TestingUtilities.Exceptions
{
    public class RidgidStringLengthAttributeNotFoundException : Exception
    {
        public override string Message { get; }

        public RidgidStringLengthAttributeNotFoundException(string fieldName)
        {
            Message = CreateErrorMessage(fieldName);
        }

        private static string CreateErrorMessage(string fieldName)
        {
            return $"The {nameof(RidgidStringLengthAttribute)} could not be found on the {fieldName} field.";
        }
    }
}