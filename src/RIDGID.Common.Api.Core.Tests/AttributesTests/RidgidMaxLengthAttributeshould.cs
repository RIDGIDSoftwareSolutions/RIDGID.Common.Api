using NUnit.Framework;
using RIDGID.Common.Api.Core.Attributes;
using RIDGID.Common.Api.Core.Utilities;
using Shouldly;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RIDGID.Common.Api.Core.Tests.AttributesTests
{
    internal class ModelWithMaxLengthFieldWithoutCustomErrorMessage
    {
        [RidgidMaxLength(1, 2)]
        public string Field { get; set; }
    }

    internal class ModelWithMaxLengthFieldWithCustomErrorMessage
    {
        [RidgidMaxLength(1, 2, "CustomMessage")]
        public string Field { get; set; }
    }

    [TestFixture]
    public class RidgidMaxLengthAttributeShould
    {
        [Test]
        public void InvalidateCorrectlyWithoutCustomErrorMessage()
        {
            //--Arrange
            var model = new ModelWithMaxLengthFieldWithoutCustomErrorMessage
            {
                Field = "123"
            };
            var validationContext = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            //--Act
            var valid = Validator.TryValidateObject(model, validationContext, result, true);

            //--Assert
            valid.ShouldBeFalse();
            result.Count.ShouldBe(1);
            var defaultErrorMsg = new MaxLengthAttribute(2).FormatErrorMessage(nameof(model.Field));
            result[0].ErrorMessage
                .ShouldBe(ModelStateCustomErrorMessage.Create(1, defaultErrorMsg));
        }

        [Test]
        public void InvalidateCorrectlyWithCustomErrorMessage()
        {
            //--Arrange
            var model = new ModelWithMaxLengthFieldWithCustomErrorMessage
            {
                Field = "123"
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
        public void ValidateCorrectlyWhenEqualToMaxLength()
        {
            //--Arrange
            var model = new ModelWithMaxLengthFieldWithoutCustomErrorMessage
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
        public void ValidateCorrectlyWhenUnderMaxLength()
        {
            //--Arrange
            var model = new ModelWithMaxLengthFieldWithoutCustomErrorMessage
            {
                Field = "1"
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
