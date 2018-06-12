using NUnit.Framework;
using RIDGID.Common.Api.Core.Utilities;

namespace RIDGID.Common.Api.Core.Tests.Utilities
{
    [TestFixture]
    public class SnakeCasePropertyNamesContractResolverShould
    {
        private SnakeCasePropertyNamesContractResolver _snakeCasePropertyNamesContractResolver;

        [SetUp]
        public void SetUp()
        {
            _snakeCasePropertyNamesContractResolver = new SnakeCasePropertyNamesContractResolver();
        }

        [Test]
        public void ConvertAnUpperCamelCasePropertyToSnakeCaseProperty()
        {
            //--Arrange
            const string expected = "tosho_toshev";
            const string input = "ToshoToshev";

            //--Act
            var result = _snakeCasePropertyNamesContractResolver.GetResolvedPropertyName(input);

            //--Assert
            Assert.AreEqual(expected, result);
        }
    }
}