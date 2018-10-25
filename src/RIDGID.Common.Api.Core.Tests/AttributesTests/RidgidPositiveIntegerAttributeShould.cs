using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RIDGID.Common.Api.Core.Attributes;
using RIDGID.Common.Api.Core.Utilities;
using Shouldly;

namespace RIDGID.Common.Api.Core.Tests.AttributesTests
{
    internal class ModelWithPositiveIntFieldWithoutCustomErrorMessage
    {
        [RidgidPositiveInteger(1)]
        public int? CustomId { get; set; }
    }

    internal class ModelWithPositiveIntFieldWithCustomErrorMessage
    {
        [RidgidPositiveInteger(1, "CustomMessage")]
        public int? CustomId { get; set; }
    }

    public class RidgidPositiveIntegerAttributeShould
    {
        [Test]
        public void InvalidateCorrectlyWithoutCustomErrorMessage()
        {
            //--Arrange
            var model = new ModelWithPositiveIntFieldWithoutCustomErrorMessage
            {
                CustomId = -1
            };
            var validationContext = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            FormatResponseMessage.SetSnakeCaseSetting(false);

            //--Act
            var valid = Validator.TryValidateObject(model, validationContext, result, true);

            //--Assert
            valid.ShouldBeFalse();
            result.Count.ShouldBe(1);
            string defaultErrorMsg = $"The 'CustomId' field must be an integer value between '0' and '{int.MaxValue}'.";
            result[0].ErrorMessage
                .ShouldBe(ModelStateCustomErrorMessage.Create(1, defaultErrorMsg));
        }

        [Test]
        public void UseSnakeCaseInDefaultErrorMessageWhenSet()
        {
            //--Arrange
            var model = new ModelWithPositiveIntFieldWithoutCustomErrorMessage
            {
                CustomId = -1
            };
            var validationContext = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            FormatResponseMessage.SetSnakeCaseSetting(true);

            //--Act
            var valid = Validator.TryValidateObject(model, validationContext, result, true);

            //--Assert
            valid.ShouldBeFalse();
            result.Count.ShouldBe(1);
            string defaultErrorMsg = $"The 'custom_id' field must be an integer value between '0' and '{int.MaxValue}'.";
            result[0].ErrorMessage
                .ShouldBe(ModelStateCustomErrorMessage.Create(1, defaultErrorMsg));
        }

        [Test]
        public void InvalidateCorrectlyWithCustomErrorMessage()
        {
            //--Arrange
            var model = new ModelWithPositiveIntFieldWithCustomErrorMessage
            {
                CustomId = -1
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
            var model = new ModelWithPositiveIntFieldWithoutCustomErrorMessage
            {
                CustomId = 1
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
