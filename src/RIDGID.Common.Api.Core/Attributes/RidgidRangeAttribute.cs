using RIDGID.Common.Api.Core.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace RIDGID.Common.Api.Core.Attributes
{
    public class RidgidRangeAttribute : RidgidValidationAttribute
    {
        public Type Type { get; set; }

        public string Mininum { get; set; }

        public string Maximum { get; set; }

        public RidgidRangeAttribute(int errorId, Type type, string mininum, string maximum) : base(errorId)
        {
            Type = type;
            Mininum = mininum;
            Maximum = maximum;
        }

        public RidgidRangeAttribute(int errorId, Type type, string mininum, string maximum, string customErrorMessage) :
            base(errorId, customErrorMessage)
        {
            Type = type;
            Mininum = mininum;
            Maximum = maximum;
        }

        public override bool IsValid(object value)
        {
            return new RangeAttribute(Type, Mininum, Maximum).IsValid(value);
        }

        public override string FormatErrorMessage(string fieldName)
        {
            var errorMessage = CustomErrorMessage ?? CreateErrorMessage(fieldName);
            return ModelStateCustomErrorMessage.Create(ErrorId, errorMessage);
        }

        private string CreateErrorMessage(string fieldName)
        {
            return $"The value of the '{fieldName}' field must be between '{Mininum}' and '{Maximum}'.";
        }
    }
}