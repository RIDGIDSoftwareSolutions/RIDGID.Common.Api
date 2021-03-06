﻿using NUnit.Framework;
using RIDGID.Common.Api.Core.Attributes;
using RIDGID.Common.Api.Core.Utilities;
using Shouldly;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace RIDGID.Common.Api.Core.Tests.AttributesTests
{
    [TestFixture]
    public class RidgidValidateModelAttributeShould
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void DoNothingWhenModelIsValid()
        {
            //--Arrange
            var attribute = new RidgidValidateModelAttribute();
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
            var attribute = new RidgidValidateModelAttribute();
            var actionContext = new HttpActionContext();
            actionContext.ModelState["Field"] = new ModelState
            {
                Errors = { ModelStateCustomErrorMessage.Create(1, "ErrorMessage") }
            };
            FormatResponseMessage.SetSnakeCaseSetting(true);

            //--Act
            attribute.OnActionExecuting(actionContext);
            var response = actionContext.Response;

            //--Assert
            var contentAsString = ContentAsString(actionContext, response);
            contentAsString.ShouldBe("{\"errors\":[{\"error_id\":1,\"debug_error_message\":\"ErrorMessage\"}]}");
        }

        [Test]
        public void ReturnSnakeCaseForTwoModelStateErrorsWhenSnakeCaseIsSet()
        {
            //--Arrange
            var attribute = new RidgidValidateModelAttribute();
            var actionContext = new HttpActionContext();
            actionContext.ModelState["Field"] = new ModelState
            {
                Errors =
                {
                    ModelStateCustomErrorMessage.Create(1, "ErrorMessage1"),
                    ModelStateCustomErrorMessage.Create(2, "ErrorMessage2")
                }
            };
            FormatResponseMessage.SetSnakeCaseSetting(true);

            //--Act
            attribute.OnActionExecuting(actionContext);
            var response = actionContext.Response;

            //--Assert
            var contentAsString = ContentAsString(actionContext, response);
            contentAsString.ShouldBe(
                "{\"errors\":[{\"error_id\":1,\"debug_error_message\":\"ErrorMessage1\"},{\"error_id\":2,\"debug_error_message\":\"ErrorMessage2\"}]}");
        }

        [Test]
        public void ReturnCamelCaseForOneModelStateErrorByDefault()
        {
            //--Arrange
            var attribute = new RidgidValidateModelAttribute();
            var actionContext = new HttpActionContext();
            actionContext.ModelState["Field"] = new ModelState
            {
                Errors = { ModelStateCustomErrorMessage.Create(1, "ErrorMessage") }
            };
            FormatResponseMessage.SetSnakeCaseSetting(false);

            //--Act
            attribute.OnActionExecuting(actionContext);
            var response = actionContext.Response;

            //--Assert
            var contentAsString = ContentAsString(actionContext, response);
            contentAsString.ShouldBe("{\"errors\":[{\"errorId\":1,\"debugErrorMessage\":\"ErrorMessage\"}]}");
        }

        [Test]
        public void ReturnCamelCaseForTwoModelStateErrorsByDefault()
        {
            //--Arrange
            var attribute = new RidgidValidateModelAttribute();
            var actionContext = new HttpActionContext();
            actionContext.ModelState["Field"] = new ModelState
            {
                Errors =
                {
                    ModelStateCustomErrorMessage.Create(1, "ErrorMessage1"),
                    ModelStateCustomErrorMessage.Create(2, "ErrorMessage2")
                }
            };
            FormatResponseMessage.SetSnakeCaseSetting(false);

            //--Act
            attribute.OnActionExecuting(actionContext);
            var response = actionContext.Response;

            //--Assert
            var contentAsString = ContentAsString(actionContext, response);
            contentAsString.ShouldBe(
                "{\"errors\":[{\"errorId\":1,\"debugErrorMessage\":\"ErrorMessage1\"},{\"errorId\":2,\"debugErrorMessage\":\"ErrorMessage2\"}]}");
        }

        [Test]
        public void ReturnCamelCaseForOneModelStateErrorWhenExplicitlyRequested()
        {
            //--Arrange
            var attribute = new RidgidValidateModelAttribute();
            var actionContext = new HttpActionContext();
            actionContext.ModelState["Field"] = new ModelState
            {
                Errors = { ModelStateCustomErrorMessage.Create(1, "ErrorMessage") }
            };
            FormatResponseMessage.SetSnakeCaseSetting(false);

            //--Act
            attribute.OnActionExecuting(actionContext);
            var response = actionContext.Response;

            //--Assert
            var contentAsString = ContentAsString(actionContext, response);
            contentAsString.ShouldBe("{\"errors\":[{\"errorId\":1,\"debugErrorMessage\":\"ErrorMessage\"}]}");
        }

        [Test]
        public void ReturnCamelCaseForTwoModelStateErrorsWhenExplicitlyRequested()
        {
            //--Arrange
            var attribute = new RidgidValidateModelAttribute();
            var actionContext = new HttpActionContext();
            actionContext.ModelState["Field"] = new ModelState
            {
                Errors =
                {
                    ModelStateCustomErrorMessage.Create(1, "ErrorMessage1"),
                    ModelStateCustomErrorMessage.Create(2, "ErrorMessage2")
                }
            };
            FormatResponseMessage.SetSnakeCaseSetting(false);

            //--Act
            attribute.OnActionExecuting(actionContext);
            var response = actionContext.Response;

            //--Assert
            var contentAsString = ContentAsString(actionContext, response);
            contentAsString.ShouldBe(
                "{\"errors\":[{\"errorId\":1,\"debugErrorMessage\":\"ErrorMessage1\"},{\"errorId\":2,\"debugErrorMessage\":\"ErrorMessage2\"}]}");
        }

        [Test]
        public void ReturnOnlyDistinctErrors()
        {
            //--Arrange
            var attribute = new RidgidValidateModelAttribute();
            var actionContext = new HttpActionContext();
            actionContext.ModelState["Field"] = new ModelState
            {
                Errors =
                {
                    ModelStateCustomErrorMessage.Create(1, "ErrorMessage1"),
                    ModelStateCustomErrorMessage.Create(1, "ErrorMessage1"),
                    ModelStateCustomErrorMessage.Create(2, "ErrorMessage2"),
                    ModelStateCustomErrorMessage.Create(2, "ErrorMessage2"),
                    ModelStateCustomErrorMessage.Create(3, "ErrorMessage3"),
                    ModelStateCustomErrorMessage.Create(4, "ErrorMessage4")
                }
            };
            FormatResponseMessage.SetSnakeCaseSetting(true);

            //--Act
            attribute.OnActionExecuting(actionContext);
            var response = actionContext.Response;

            //--Assert
            var contentAsString = ContentAsString(actionContext, response);
            contentAsString.ShouldBe(
                "{\"errors\":[{\"error_id\":1,\"debug_error_message\":\"ErrorMessage1\"},{\"error_id\":2,\"debug_error_message\":\"ErrorMessage2\"},{\"error_id\":3,\"debug_error_message\":\"ErrorMessage3\"},{\"error_id\":4,\"debug_error_message\":\"ErrorMessage4\"}]}");
        }

        private static string ContentAsString(HttpActionContext actionContext, HttpResponseMessage response)
        {
            actionContext.ModelState.Count.ShouldBe(1);
            response.ShouldNotBeNull();
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            var contentAsString = response.Content.ReadAsStringAsync().Result;
            contentAsString.ShouldNotBeNull();
            contentAsString.Length.ShouldBeGreaterThan(0);
            return contentAsString;
        }
    }
}