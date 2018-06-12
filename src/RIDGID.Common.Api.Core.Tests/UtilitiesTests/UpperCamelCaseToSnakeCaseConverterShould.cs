using NUnit.Framework;
using RIDGID.Common.Api.Core.Utilities;

namespace RIDGID.Common.Api.Core.Tests.Utilities
{
    [TestFixture]
    public class UpperCamelCaseToSnakeCaseConverterShould
    {
        [Test]
        public void ConvertASnakeCasePropertyToUpperCamelCase()
        {
            //--Arrange
            const string snakeCaseProperty = "my_snake_case_property";
            const string expectedUpperCamelCaseProperty = "MySnakeCaseProperty";

            //--Act
            var result = UpperCamelCaseToSnakeCaseConverter.FromSnakeCase(snakeCaseProperty);

            //--Assert
            Assert.AreEqual(expectedUpperCamelCaseProperty, result);
        }

        [Test]
        public void ConvertAnUpperCamelCasePropertyToSnakeCaseProperty()
        {
            //--Arrange
            const string upperCaseProperty = "MyUpperCamelCaseProperty";
            const string expectedSnakeCaseProperty = "my_upper_camel_case_property";

            //--Act
            var result = UpperCamelCaseToSnakeCaseConverter.ToSnakeCase(upperCaseProperty);

            //--Assert
            Assert.AreEqual(expectedSnakeCaseProperty, result);
        }
    }
}