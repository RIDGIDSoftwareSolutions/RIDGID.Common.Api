using NUnit.Framework;
using RIDGID.Common.Api.Core.Utilities;

namespace RIDGID.Common.Api.Core.Tests.Utilities
{
    [TestFixture]
    public class CamelCaseToSnakeCaseConverterShould
    {
        [Test]
        public void ConvertALowerCamelCasePropertyToSnakeCase()
        {
            //--Arrange
            const string lowerCamelCaseProperty = "myLowerCaseProperty";
            const string expectedSnakeCaseProperty = "my_lower_case_property";

            //--Act
            var result = CamelCaseToSnakeCaseConverter.ToSnakeCase(lowerCamelCaseProperty);

            //--Assert
            Assert.AreEqual(expectedSnakeCaseProperty, result);
        }

        [Test]
        public void ConvertASnakeCasePropertyToLowerCamelCase()
        {
            //--Arrange
            const string snakeCaseProperty = "my_snake_case_property";
            const string expectedLowerCamelCaseProperty = "mySnakeCaseProperty";

            //--Act
            var result = CamelCaseToSnakeCaseConverter.FromSnakeCase(snakeCaseProperty);

            //--Assert
            Assert.AreEqual(expectedLowerCamelCaseProperty, result);
        }
    }
}