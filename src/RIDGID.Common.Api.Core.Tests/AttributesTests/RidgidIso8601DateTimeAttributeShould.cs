using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using RIDGID.Common.Api.Core.Attributes;
using RIDGID.Common.Api.Core.Utilities;
using Shouldly;

namespace RIDGID.Common.Api.Core.Tests.AttributesTests
{
    internal class ModelWithDateTimeFieldWithoutCustomErrorMessage
    {
        [RidgidIso8601DateTime(1)] public string CreatedDate { get; set; }
    }

    internal class ModelWithDateTimeFieldWithCustomErrorMessage
    {
        [RidgidIso8601DateTime(1, "CustomMessage")]
        public string CreatedDate { get; set; }
    }

    [TestFixture]
    public class RidgidIso8601DateTimeAttributeShould
    {
        [Test]
        public void InvalidateCorrectlyWithoutCustomErrorMessage()
        {
            //--Arrange
            var model = new ModelWithDateTimeFieldWithoutCustomErrorMessage
            {
                CreatedDate = "invalidDate"
            };
            var validationContext = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            FormatResponseMessage.SetSnakeCaseSetting(false);

            //--Act
            var valid = Validator.TryValidateObject(model, validationContext, result, true);

            //--Assert
            valid.ShouldBeFalse();
            result.Count.ShouldBe(1);
            const string defaultErrorMsg = "The 'CreatedDate' must be in ISO-8601 format, (i.e. yyyy-mm-ddThh:mm:ss.ffffff).";
            result[0].ErrorMessage
                .ShouldBe(ModelStateCustomErrorMessage.Create(1, defaultErrorMsg));
        }

        [Test]
        public void InvalidateCorrectlyWithCustomErrorMessage()
        {
            //--Arrange
            var model = new ModelWithDateTimeFieldWithCustomErrorMessage
            {
                CreatedDate = "invalidDate"
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
        public void ValidateCorrectlyForDate()
        {
            //--Arrange
            var model = new ModelWithDateTimeFieldWithoutCustomErrorMessage
            {
                CreatedDate = "2018-06-09T07:35:16Z"
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
        public void ValidateCorrectlyForOptionalDate()
        {
            //--Arrange
            var model = new ModelWithDateTimeFieldWithoutCustomErrorMessage
            {
                CreatedDate = null
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
            var model = new ModelWithDateTimeFieldWithoutCustomErrorMessage
            {
                CreatedDate = "invalidDate"
            };
            var validationContext = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            FormatResponseMessage.SetSnakeCaseSetting(true);

            //--Act
            var valid = Validator.TryValidateObject(model, validationContext, result, true);

            //--Assert
            valid.ShouldBeFalse();
            result.Count.ShouldBe(1);
            const string defaultErrorMsg = "The 'created_date' must be in ISO-8601 format, (i.e. yyyy-mm-ddThh:mm:ss.ffffff).";
            result[0].ErrorMessage
                .ShouldBe(ModelStateCustomErrorMessage.Create(1, defaultErrorMsg));
        }
    }
}
