using System.Collections.Generic;
using NUnit.Framework;
using RIDGID.Common.Api.Core.Attributes;
using RIDGID.Common.Api.TestingUtilities.Exceptions;
using RIDGID.Common.Api.TestingUtilities.FieldValidations;
using Shouldly;

namespace RIDGID.Common.Api.TestingUtilities.Tests
{
    [TestFixture]
    public class ShouldValidateTheseFieldsExtensionShould
    {
        [Test]
        public void ThrowFieldNotFoundExceptionIfFieldNameNotFoundOnModel()
        {
            //--Arrange
            var model = new object();

            //--Act/Assert
            Should.Throw<FieldNotFoundException>(() =>
                model.ShouldValidateTheseFields(new List<RidgidFieldValidation>
                {
                    new RidgidFieldValidation
                    {
                        FieldName = "NotFoundFieldName",
                        ErrorId = 1
                    }
                }));
        }

        [Test]
        public void FailIfRidgidEmailAddressAttributeIsMissingFromFieldName()
        {
            //--Arrange
            var model = new { Field = 1 };

            //--Act
            Should.Throw<RidgidEmailAddressAttributeNotFoundException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidEmailAddressFieldValidation
                    {
                        FieldName = nameof(model.Field)
                    }
                }));
        }

        [Test]
        public void FailIfRidgidEmailAddressAttributeHasWrongErrorId()
        {

            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            Should.Throw<ShouldAssertException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidEmailAddressFieldValidation
                    {
                        ErrorId = 2,
                        FieldName = nameof(model.Email)
                    }
                }));
        }

        [Test]
        public void FailIfRidgidEmailAddressAttributeHasWrongErrorMessage()
        {

            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            Should.Throw<ShouldAssertException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidEmailAddressFieldValidation
                    {
                        ErrorId = 1,
                        ErrorMessage = "ExpectedErrorMessage",
                        FieldName = nameof(model.Email)
                    }
                }));
        }

        [Test]
        public void PassIfRidgidEmailAddressMatches()
        {

            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidEmailAddressFieldValidation
                    {
                        ErrorId = 1,
                        ErrorMessage = "ActualErrorMessage",
                        FieldName = nameof(model.Email)
                    }
                });
        }

        [Test]
        public void FailIfRidgidMaxLengthAttributeIsMissingFromFieldName()
        {
            //--Arrange
            var model = new { Field = 1 };

            //--Act
            Should.Throw<RidgidMaxLengthAttributeNotFoundException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidMaxLengthFieldValidation
                    {
                        FieldName = nameof(model.Field)
                    }
                }));
        }

        [Test]
        public void FailIfRidgidMaxLengthAttributeHasWrongErrorId()
        {
            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            Should.Throw<ShouldAssertException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidMaxLengthFieldValidation
                    {
                        ErrorId = 2,
                        FieldName = nameof(model.MaxLength)
                    }
                }));
        }

        [Test]
        public void FailIfRidgidMaxLengthAttributeHasWrongErrorMessage()
        {
            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            Should.Throw<ShouldAssertException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidMaxLengthFieldValidation
                    {
                        ErrorId = 1,
                        ErrorMessage = "ExpectedErrorMessage",
                        FieldName = nameof(model.MaxLength),
                    }
                }));
        }

        [Test]
        public void FailIfRidgidMaxLengthAttributeHasWrongLength()
        {
            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            Should.Throw<ShouldAssertException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidMaxLengthFieldValidation
                    {
                        ErrorId = 1,
                        ErrorMessage = "ActualErrorMessage",
                        FieldName = nameof(model.MaxLength),
                        MaxLength = 2
                    }
                }));
        }

        [Test]
        public void PassIfRidgidMaxLengthAttributeMatches()
        {
            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidMaxLengthFieldValidation
                    {
                        ErrorId = 1,
                        ErrorMessage = "ActualErrorMessage",
                        FieldName = nameof(model.MaxLength),
                        MaxLength = 1
                    }
                });
        }

        [Test]
        public void FailIfRidgidMinLengthAttributeIsMissingFromFieldName()
        {
            //--Arrange
            var model = new { Field = 1 };

            //--Act
            Should.Throw<RidgidMinLengthAttributeNotFoundException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidMinLengthFieldValidation
                    {
                        FieldName = nameof(model.Field)
                    }
                }));
        }

        [Test]
        public void FailIfRidgidMinLengthAttributeHasWrongErrorId()
        {
            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            Should.Throw<ShouldAssertException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidMinLengthFieldValidation
                    {
                        ErrorId = 2,
                        FieldName = nameof(model.MinLength)
                    }
                }));
        }

        [Test]
        public void FailIfRidgidMinLengthAttributeHasWrongErrorMessage()
        {
            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            Should.Throw<ShouldAssertException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidMinLengthFieldValidation
                    {
                        ErrorId = 1,
                        ErrorMessage = "ExpectedErrorMessage",
                        FieldName = nameof(model.MinLength),
                    }
                }));
        }

        [Test]
        public void FailIfRidgidMinLengthAttributeHasWrongLength()
        {
            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            Should.Throw<ShouldAssertException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidMinLengthFieldValidation
                    {
                        ErrorId = 1,
                        ErrorMessage = "ActualErrorMessage",
                        FieldName = nameof(model.MinLength),
                        MinLength = 2
                    }
                }));
        }

        [Test]
        public void PassIfRidgidMinLengthAttributeMatches()
        {
            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidMinLengthFieldValidation
                    {
                        ErrorId = 1,
                        ErrorMessage = "ActualErrorMessage",
                        FieldName = nameof(model.MinLength),
                        MinLength = 1
                    }
                });
        }

        [Test]
        public void FailIfRidgidRegularExpressionAttributeIsMissingFromFieldName()
        {
            //--Arrange
            var model = new { Field = 1 };

            //--Act
            Should.Throw<RidgidRegularExpressionAttributeNotFoundException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidRegularExpressionFieldValidation
                    {
                        FieldName = nameof(model.Field)
                    }
                }));
        }

        [Test]
        public void FailIfRidgidRegularExpressionAttributeHasWrongErrorId()
        {
            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            Should.Throw<ShouldAssertException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidRegularExpressionFieldValidation
                    {
                        ErrorId = 2,
                        FieldName = nameof(model.RegularExpression)
                    }
                }));
        }

        [Test]
        public void FailIfRidgidRegularExpressionAttributeHasWrongErrorMessage()
        {
            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            Should.Throw<ShouldAssertException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidRegularExpressionFieldValidation
                    {
                        ErrorId = 1,
                        ErrorMessage = "ExpectedErrorMessage",
                        FieldName = nameof(model.RegularExpression),
                    }
                }));
        }

        [Test]
        public void FailIfRidgidRegularExpressionAttributeHasWrongRegex()
        {
            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            Should.Throw<ShouldAssertException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidRegularExpressionFieldValidation
                    {
                        ErrorId = 1,
                        ErrorMessage = "ActualErrorMessage",
                        FieldName = nameof(model.RegularExpression),
                        Regex = "wrongregex"
                    }
                }));
        }

        [Test]
        public void PassIfRidgidRegularExpressionAttributeMatches()
        {
            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidRegularExpressionFieldValidation
                    {
                        ErrorId = 1,
                        ErrorMessage = "ActualErrorMessage",
                        FieldName = nameof(model.RegularExpression),
                        Regex = "a|b"
                    }
                });
        }

        [Test]
        public void FailIfRidgidRequiredAttributeIsMissingFromFieldName()
        {
            //--Arrange
            var model = new { Field = 1 };

            //--Act
            Should.Throw<RidgidRequiredAttributeNotFoundException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidRequiredFieldValidation
                    {
                        FieldName = nameof(model.Field)
                    }
                }));
        }

        [Test]
        public void FailIfRidgidRequiredAttributeHasWrongErrorId()
        {
            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            Should.Throw<ShouldAssertException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidRequiredFieldValidation
                    {
                        ErrorId = 2,
                        FieldName = nameof(model.Required)
                    }
                }));
        }

        [Test]
        public void FailIfRidgidRequiredAttributeHasWrongErrorMessage()
        {
            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            Should.Throw<ShouldAssertException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidRequiredFieldValidation
                    {
                        ErrorId = 1,
                        ErrorMessage = "ExpectedErrorMessage",
                        FieldName = nameof(model.Required),
                    }
                }));
        }

        [Test]
        public void PassIfRidgidRequiredAttributeMatches()
        {
            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidRequiredFieldValidation
                    {
                        ErrorId = 1,
                        ErrorMessage = "ActualErrorMessage",
                        FieldName = nameof(model.Required),
                    }
                });
        }

        [Test]
        public void FailIfRidgidStringLengthAttributeIsMissingFromFieldName()
        {
            //--Arrange
            var model = new { Field = 1 };

            //--Act
            Should.Throw<RidgidStringLengthAttributeNotFoundException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidStringLengthFieldValidation
                    {
                        FieldName = nameof(model.Field)
                    }
                }));
        }

        [Test]
        public void FailIfRidgidStringLengthAttributeHasWrongErrorId()
        {
            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            Should.Throw<ShouldAssertException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidStringLengthFieldValidation
                    {
                        ErrorId = 2,
                        FieldName = nameof(model.StringLength)
                    }
                }));
        }

        [Test]
        public void FailIfRidgidStringLengthAttributeHasWrongErrorMessage()
        {
            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            Should.Throw<ShouldAssertException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidStringLengthFieldValidation
                    {
                        ErrorId = 1,
                        ErrorMessage = "ExpectedErrorMessage",
                        FieldName = nameof(model.StringLength),
                    }
                }));
        }

        [Test]
        public void FailIfRidgidStringLengthAttributeHasWrongMinLength()
        {
            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            Should.Throw<ShouldAssertException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidStringLengthFieldValidation
                    {
                        ErrorId = 1,
                        ErrorMessage = "ExpectedErrorMessage",
                        FieldName = nameof(model.StringLength),
                        MinLength = 0
                    }
                }));
        }

        [Test]
        public void FailIfRidgidStringLengthAttributeHasWrongMaxLength()
        {
            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            Should.Throw<ShouldAssertException>(() => model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidStringLengthFieldValidation
                    {
                        ErrorId = 1,
                        ErrorMessage = "ExpectedErrorMessage",
                        FieldName = nameof(model.StringLength),
                        MinLength = 0,
                        MaxLength = 0
                    }
                }));
        }

        [Test]
        public void PassIfRidgidStringLengthAttributeMatches()
        {
            //--Arrange
            var model = new ShouldValidateExtensionTestModel();

            //--Act
            model.ShouldValidateTheseFields(
                new List<RidgidFieldValidation>
                {
                    new RidgidStringLengthFieldValidation
                    {
                        ErrorId = 1,
                        ErrorMessage = "ActualErrorMessage",
                        FieldName = nameof(model.StringLength),
                        MinLength = 1,
                        MaxLength = 2
                    }
                });
        }
    }

    internal class ShouldValidateExtensionTestModel
    {
        [RidgidEmailAddress(1, "ActualErrorMessage")]
        public string Email { get; set; }

        [RidgidMaxLength(1, 1, "ActualErrorMessage")]
        public string MaxLength { get; set; }

        [RidgidMinLength(1, 1, "ActualErrorMessage")]
        public string MinLength { get; set; }

        [RidgidRegularExpression(1, "a|b", "ActualErrorMessage")]
        public string RegularExpression { get; set; }

        [RidgidRequired(1, "ActualErrorMessage")]
        public string Required { get; set; }

        [RidgidStringLength(1, 1, 2, "ActualErrorMessage")]
        public string StringLength { get; set; }
    }
}