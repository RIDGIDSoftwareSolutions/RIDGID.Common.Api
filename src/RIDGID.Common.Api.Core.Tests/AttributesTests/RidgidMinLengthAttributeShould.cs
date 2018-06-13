using NUnit.Framework;
using RIDGID.Common.Api.Core.Attributes;
using RIDGID.Common.Api.Core.Utilities;
using Shouldly;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RIDGID.Common.Api.Core.Tests.AttributesTests
{
    internal class ModelWithMinLengthFieldWithoutCustomErrorMessage
    {
        [RidgidMinLength(1, 2)]
        public string Field { get; set; }
    }

    internal class ModelWithMinLengthFieldWithCustomErrorMessage
    {
        [RidgidMinLength(1, 2, "CustomMessage")]
        public string Field { get; set; }
    }

    [TestFixture]
    public class RidgidMinLengthAttributeShould
    {
        [Test]
        public void InvalidateCorrectlyWithoutCustomErrorMessage()
        {
            //--Arrange
            var model = new ModelWithMinLengthFieldWithoutCustomErrorMessage
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
            var defaultErrorMsg = new MinLengthAttribute(2).FormatErrorMessage(nameof(model.Field));
            result[0].ErrorMessage
                .ShouldBe(ModelStateCustomErrorMessage.Create(1, defaultErrorMsg));
        }

        [Test]
        public void InvalidateCorrectlyWithCustomErrorMessage()
        {
            //--Arrange
            var model = new ModelWithMinLengthFieldWithCustomErrorMessage
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
            result[0].ErrorMessage
                .ShouldBe(ModelStateCustomErrorMessage.Create(1, "CustomMessage"));
        }

        [Test]
        public void ValidateCorrectlyWhenEqualToMinLength()
        {
            //--Arrange
            var model = new ModelWithMinLengthFieldWithoutCustomErrorMessage
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
        public void ValidateCorrectlyWhenOverMinLength()
        {
            //--Arrange
            var model = new ModelWithMinLengthFieldWithoutCustomErrorMessage
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
    }
}
