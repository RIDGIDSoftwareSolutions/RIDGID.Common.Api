using System;

namespace RIDGID.Common.Api.TestingUtilities.FieldValidations
{
    public class RidgidRangeFieldValidation : RidgidFieldValidation
    {
        public string Maximum { get; set; }

        public string Mininum { get; set; }

        public Type Type { get; set; }

        public RidgidRangeFieldValidation()
        {
            ValidationType = RidgidValidationType.RangeAttribute;
        }
    }
}
