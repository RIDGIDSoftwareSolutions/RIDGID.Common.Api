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
        public string MultipleWordedField { get; set; }
    }

    internal class ModelWithStringLengthMinAndMaxEqualField
    {
        [RidgidStringLength(1, 2, 2)]
        public string MultipleWordedField { get; set; }
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
                MultipleWordedField = "1"
            };
            var validationContext = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            FormatResponseMessage.SetSnakeCaseSetting(false);

            //--Act
            var valid = Validator.TryValidateObject(model, validationContext, result, true);

            //--Assert
            valid.ShouldBeFalse();
            result.Count.ShouldBe(1);
            const string defaultErrorMsg = "The 'MultipleWordedField' field must be between '2' and '3' characters long.";
            result[0].ErrorMessage.ShouldBe(ModelStateCustomErrorMessage.Create(1, defaultErrorMsg));
        }

        [Test]
        public void InvalidateCorrectlyWhenMoreThanMaxLength()
        {
            //--Arrange
            var model = new ModelWithStringLengthField
            {
                MultipleWordedField = "1234"
            };
            var validationContext = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            FormatResponseMessage.SetSnakeCaseSetting(false);

            //--Act
            var valid = Validator.TryValidateObject(model, validationContext, result, true);

            //--Assert
            valid.ShouldBeFalse();
            result.Count.ShouldBe(1);
            const string defaultErrorMsg = "The 'MultipleWordedField' field must be between '2' and '3' characters long.";
            result[0].ErrorMessage.ShouldBe(ModelStateCustomErrorMessage.Create(1, defaultErrorMsg));
        }

        [Test]
        public void UseSnakeCaseForFieldNameWhenSet()
        {
            //--Arrange
            var model = new ModelWithStringLengthField
            {
                MultipleWordedField = "1"
            };
            var validationContext = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            FormatResponseMessage.SetSnakeCaseSetting(true);

            //--Act
            var valid = Validator.TryValidateObject(model, validationContext, result, true);

            //--Assert
            valid.ShouldBeFalse();
            result.Count.ShouldBe(1);
            const string defaultErrorMsg = "The 'multiple_worded_field' field must be between '2' and '3' characters long.";
            result[0].ErrorMessage.ShouldBe(ModelStateCustomErrorMessage.Create(1, defaultErrorMsg));
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

            FormatResponseMessage.SetSnakeCaseSetting(false);

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
                MultipleWordedField = "12"
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
                MultipleWordedField = "123"
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
                MultipleWordedField = "12"
            };
            var validationContext = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            FormatResponseMessage.SetSnakeCaseSetting(false);

            //--Act
            var valid = Validator.TryValidateObject(model, validationContext, result, true);

            //--Assert
            valid.ShouldBeTrue();
            result.Count.ShouldBe(0);
        }

        [Test]
        public void InvalidateCorrectlyWhenMaxAndMinEqualWhenNotSnakeCase()
        {
            //--Arrange
            var model = new ModelWithStringLengthMinAndMaxEqualField
            {
                MultipleWordedField = "1234"
            };
            var validationContext = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            FormatResponseMessage.SetSnakeCaseSetting(true);

            //--Act
            var valid = Validator.TryValidateObject(model, validationContext, result, true);

            //--Assert
            valid.ShouldBeFalse();
            result.Count.ShouldBe(1);
            result[0].ErrorMessage.ShouldBe(ModelStateCustomErrorMessage.Create(1,
                "The 'multiple_worded_field' field must be '2' characters long."));
        }


        [Test]
        public void InvalidateCorrectlyWhenMaxAndMinEqualWhenSnakeCase()
        {
            //--Arrange
            var model = new ModelWithStringLengthMinAndMaxEqualField
            {
                MultipleWordedField = "1234"
            };
            var validationContext = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            FormatResponseMessage.SetSnakeCaseSetting(true);

            //--Act
            var valid = Validator.TryValidateObject(model, validationContext, result, true);

            //--Assert
            valid.ShouldBeFalse();
            result.Count.ShouldBe(1);
            result[0].ErrorMessage.ShouldBe(ModelStateCustomErrorMessage.Create(1,
                "The 'multiple_worded_field' field must be '2' characters long."));
        }
    }
}