using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using NUnit.Framework;
using RIDGID.Common.Api.Core.Attributes;
using RIDGID.Common.Api.Core.Utilities;
using RIDGID.Common.Api.TestingUtilities;
using Shouldly;

namespace RIDGID.Common.Api.Core.Tests.AttributesTests
{
    [TestFixture]
    public class RidgidModelValidationAttributeShould
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void DoNothingWhenModelIsValid()
        {
            //--Arrange
            var attribute = new RidgidModelValidationAttribute();
            var actionContext = new HttpActionContext();

            //--Act
            attribute.OnActionExecuting(actionContext);

            //--Assert
            actionContext.Response.ShouldBe(null);
        }

        [Test]
        public void ReturnSnakeCaseForOneModelStateErrorWhenSnakeCaseIsSet()
        {
            //--Arrange
            var attribute = new RidgidModelValidationAttribute();
            var actionContext = new HttpActionContext();
            actionContext.ModelState["Field"] = new ModelState
            {
                Errors = {  ModelValidationUtilities.CreateSpecialModelValidationMessage(1, "ErrorMessage") }
            };

            //--Act
            attribute.OnActionExecuting(actionContext);
            var response = actionContext.Response;

            //--Assert
            actionContext.ModelState.Count.ShouldBe(1);
            response.ShouldNotBeNull();
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            var contentAsString = response.Content.ReadAsStringAsync().Result;
            contentAsString.ShouldNotBeNull();
            contentAsString.Length.ShouldBeGreaterThan(0);
            var errorsResponse = Deserialize.DeserializeErrorResponse(contentAsString, 1);
            errorsResponse.Errors.Count.ShouldBe(1);
            errorsResponse.Errors[0].DebugErrorMessage.ShouldBe("ErrorMessage");
            errorsResponse.Errors[0].ErrorId.ShouldBe(1);
        }
    }
}