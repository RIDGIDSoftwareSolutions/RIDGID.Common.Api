using NUnit.Framework;
using Shouldly;
using System.Configuration;

namespace RIDGID.Common.Api.TestingUtilities.Tests
{
    [TestFixture]
    public class TestTests
    {
        [Test]
        public void TestErrorResponseDeserializesTwoCamelCaseJsonErrorsCorrectly()
        {
            //--Arrange
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["snakecase"].Value = "false";
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            const string json =
                "{\"Errors\":[{\"ErrorId\":1,\"DebugErrorMessage\":\"ErrorMessage1\"},{\"ErrorId\":2,\"DebugErrorMessage\":\"ErrorMessage2\"}]}";

            //--Act/Assert
            var errorsResponse = Test.ErrorsResponse(json, 2);
            errorsResponse.Errors[0].ErrorId.ShouldBe(1);
            errorsResponse.Errors[0].DebugErrorMessage.ShouldBe("ErrorMessage1");
            errorsResponse.Errors[1].ErrorId.ShouldBe(2);
            errorsResponse.Errors[1].DebugErrorMessage.ShouldBe("ErrorMessage2");
        }

        [Test]
        public void TestErrorResponseDeserializesTwoSnakeCaseJsonErrorsCorrectly()
        {
            //--Arrange
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["snakecase"].Value = "true";
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            const string json =
                "{\"errors\":[{\"error_id\":1,\"debug_error_message\":\"ErrorMessage1\"},{\"error_id\":2,\"debug_error_message\":\"ErrorMessage2\"}]}";

            //--Act/Assert
            var errorsResponse = Test.ErrorsResponse(json, 2);
            errorsResponse.Errors[0].ErrorId.ShouldBe(1);
            errorsResponse.Errors[0].DebugErrorMessage.ShouldBe("ErrorMessage1");
            errorsResponse.Errors[1].ErrorId.ShouldBe(2);
            errorsResponse.Errors[1].DebugErrorMessage.ShouldBe("ErrorMessage2");
        }
    }
}
