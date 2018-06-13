using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using RIDGID.Common.Api.Core.Attributes;
using RIDGID.Common.Api.Core.Utilities;
using Shouldly;

namespace RIDGID.Common.Api.Core.Tests.AttributesTests
{
    internal class ModelWithRequiredFieldWithoutCustomErrorMessage
    {
        [RidgidRequired(1)]
        public int? Field { get; set; }
    }

    internal class ModelWithRequiredFieldWithCustomErrorMessage
    {
        [RidgidRequired(1, "CustomMessage")]
        public int? Field { get; set; }
    }

    [TestFixture]
    public class RidgidRequiredAttributeShould
    {
        [Test]
        public void InvalidateCorrectlyWithoutCustomErrorMessage()
        {
            //--Arrange
            var model = new ModelWithRequiredFieldWithoutCustomErrorMessage();
            var validationContext = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            //--Act
            var valid = Validator.TryValidateObject(model, validationContext, result, true);

            //--Assert
            valid.ShouldBeFalse();
            result.Count.ShouldBe(1);
            var defaultErrorMsg = new RequiredAttribute().FormatErrorMessage(nameof(model.Field));
            result[0].ErrorMessage
                .ShouldBe(ModelStateCustomErrorMessage.Create(1, defaultErrorMsg));
        }

        [Test]
        public void InvalidateCorrectlyWithCustomErrorMessage()
        {
            //--Arrange
            var model = new ModelWithRequiredFieldWithCustomErrorMessage();
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
            var model = new ModelWithRequiredFieldWithoutCustomErrorMessage
            {
                Field = 1
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
