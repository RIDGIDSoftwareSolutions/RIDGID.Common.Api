using NUnit.Framework;
using RIDGID.Common.Api.Core.Attributes;
using RIDGID.Common.Api.Core.Utilities;
using Shouldly;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RIDGID.Common.Api.Core.Tests.AttributesTests
{
    internal class ModelWithEmailAddressFieldWithoutCustomErrorMessage
    {
        [RidgidEmailAddress(1)] public string MultipleWordedField { get; set; }
    }

    internal class ModelWithEmailAddressFieldWithCustomErrorMessage
    {
        [RidgidEmailAddress(1, "CustomMessage")]
        public string Field { get; set; }
    }

    [TestFixture]
    public class RidgidEmailAddressAttributeShould
    {
        [Test]
        public void InvalidateCorrectlyWithoutCustomErrorMessage()
        {
            //--Arrange
            var model = new ModelWithEmailAddressFieldWithoutCustomErrorMessage
            {
                MultipleWordedField = "invalidemail"
            };
            var validationContext = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            FormatResponseMessage.SetSnakeCaseSetting(false);

            //--Act
            var valid = Validator.TryValidateObject(model, validationContext, result, true);

            //--Assert
            valid.ShouldBeFalse();
            result.Count.ShouldBe(1);
            const string defaultErrorMsg = "The 'MultipleWordedField' field is an invalid email address.";
            result[0].ErrorMessage
                .ShouldBe(ModelStateCustomErrorMessage.Create(1, defaultErrorMsg));
        }

        [Test]
        public void InvalidateCorrectlyWithCustomErrorMessage()
        {
            //--Arrange
            var model = new ModelWithEmailAddressFieldWithCustomErrorMessage
            {
                Field = "invalidemail"
            };
            var validationContext = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            //--Act
            var valid = Validator.TryValidateObject(model, validationContext, result, true);

            //--Assert
            valid.ShouldBeFalse();
            result.Count.ShouldBe(1);
            result[0].ErrorMessage
                .ShouldBe(ModelStateCustomErrorMessage.Create(1, "CustomMessage"));
        }

        [Test]
        public void ValidateCorrectly()
        {
            //--Arrange
            var model = new ModelWithEmailAddressFieldWithoutCustomErrorMessage
            {
                MultipleWordedField = "a@a.com"
            };
            var validationContext = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            //--Act
            var valid = Validator.TryValidateObject(model, validationContext, result, true);

            //--Assert
            valid.ShouldBeTrue();
            result.Count.ShouldBe(0);
        }

        [Test]
        public void ConvertFieldNameToSnakeCaseIfSetInAppConfig()
        {
            //--Arrange
            var model = new ModelWithEmailAddressFieldWithoutCustomErrorMessage()
            {
                MultipleWordedField = "invalidemail"
            };
            var validationContext = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            FormatResponseMessage.SetSnakeCaseSetting(true);

            //--Act
            var valid = Validator.TryValidateObject(model, validationContext, result, true);

            //--Assert
            valid.ShouldBeFalse();
            result.Count.ShouldBe(1);
            const string defaultErrorMsg = "The 'multiple_worded_field' field is an invalid email address.";
            result[0].ErrorMessage
                .ShouldBe(ModelStateCustomErrorMessage.Create(1, defaultErrorMsg));
        }
    }
}
