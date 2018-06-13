using NUnit.Framework;
using RIDGID.Common.Api.Core.Attributes;
using RIDGID.Common.Api.Core.Utilities;
using Shouldly;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RIDGID.Common.Api.Core.Tests.AttributesTests
{
    internal class ModelWithStringLengthField
    {
        [RidgidStringLength(1, 2, 3)]
        public string Field { get; set; }
    }

    internal class ModelWithStringLengthMinAndMaxEqualField
    {
        [RidgidStringLength(1, 2, 2)]
        public string Field { get; set; }
    }

    internal class ModelWithStringLengthFieldWithCustomErrorMessage
    {
        [RidgidStringLength(1, 2, 3, "CustomMessage")]
        public string Field { get; set; }
    }

    [TestFixture]
    public class RidgidStringLengthAttributeShould
    {
        [Test]
        public void InvalidateCorrectlyWhenLessThanMinLength()
        {
            //--Arrange
            var model = new ModelWithStringLengthField
            {
                Field = "1"
            };
            var validationContext = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            //--Act
            var valid = Validator.TryValidateObject(model, validationContext, result, true);

            //--Assert
            valid.ShouldBeFalse();
            result.Count.ShouldBe(1);
            var defaultErrorMsg = new RidgidStringLengthAttribute(1, 2, 3).FormatErrorMessage(nameof(model.Field));
            result[0].ErrorMessage.ShouldBe(defaultErrorMsg);
        }

        public void InvalidateCorrectlyWhenMoreThanMaxLength()
        {
            //--Arrange
            var model = new ModelWithStringLengthField
            {
                Field = "1234"
            };
            var validationContext = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            //--Act
            var valid = Validator.TryValidateObject(model, validationContext, result, true);

            //--Assert
            valid.ShouldBeFalse();
            result.Count.ShouldBe(1);
            var defaultErrorMsg = new RidgidStringLengthAttribute(1, 2, 3).FormatErrorMessage(nameof(model.Field));
            result[0].ErrorMessage.ShouldBe(defaultErrorMsg);
        }

        [Test]
        public void InvalidateCorrectlyWithCustomErrorMessage()
        {
            //--Arrange
            var model = new ModelWithStringLengthFieldWithCustomErrorMessage
            {
                Field = "invalid"
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
        public void ValidateCorrectlyWhenEqualToMinLength()
        {
            //--Arrange
            var model = new ModelWithStringLengthField
            {
                Field = "12"
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
        public void ValidateCorrectlyWhenEqualToMaxLength()
        {
            //--Arrange
            var model = new ModelWithStringLengthField
            {
                Field = "123"
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
        public void ValidateCorrectlyWhenMaxAndMinEqual()
        {
            //--Arrange
            var model = new ModelWithStringLengthMinAndMaxEqualField
            {
                Field = "12"
            };
            var validationContext = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            //--Act
            var valid = Validator.TryValidateObject(model, validationContext, result, true);

            //--Assert
            valid.ShouldBeTrue();
            result.Count.ShouldBe(0);
        }
    }
}