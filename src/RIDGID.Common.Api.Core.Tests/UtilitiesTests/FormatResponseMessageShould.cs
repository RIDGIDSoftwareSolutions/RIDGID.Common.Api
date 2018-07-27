using NUnit.Framework;
using RIDGID.Common.Api.Core.Utilities;
using Shouldly;
using System.Configuration;

namespace RIDGID.Common.Api.Core.Tests.UtilitiesTests
{
    [TestFixture]
    public class FormatResponseMessageShould
    {
        [Test]
        public void IsSnakeCaseReturnsFalseWithNoAppConfigSnakeCaseSetting()
        {
            //--Act/Assert
            FormatResponseMessage.IsSnakeCase().ShouldBeFalse();
        }

        [Test]
        public void IsSnakeCaseReturnsTrueWithAppConfigSnakeCaseSettingTrue()
        {
            //--Arrange
            FormatResponseMessage.SetSnakeCaseSetting(true);

            //--Act/Assert
            FormatResponseMessage.IsSnakeCase().ShouldBeTrue();
        }


        [Test]
        public void IsSnakeCaseReturnsFalseWithAppConfigSnakeCaseSettingFalse()
        {
            //--Arrange
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["snakecase"].Value = "false";
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            //--Act/Assert
            FormatResponseMessage.IsSnakeCase().ShouldBeFalse();
        }

        [Test]
        public void GetCasing()
        {
            //--Arrange
            const string camelCaseStr = "HelloMyNameIsBob";
            const string expectedSnakeCase = "hello_my_name_is_bob";

            //--Act
            var result = FormatResponseMessage.ConvertCamelCaseIntoSnakeCase(camelCaseStr);

            //--Assert
            result.ShouldBe(expectedSnakeCase);
        }
    }
}