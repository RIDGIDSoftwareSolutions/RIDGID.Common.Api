using RIDGID.Common.Api.Core.Objects;
using RIDGID.Common.Api.Core.Utilities;
using Shouldly;
using System.Net;
using System.Threading;
using System.Web.Http;

namespace RIDGID.Common.Api.TestingUtilities
{
    public class Test
    {
        public static string Content(IHttpActionResult actionResult, HttpStatusCode expectedStatusCode)
        {
            var result = actionResult.ExecuteAsync(new CancellationToken()).Result;
            result.StatusCode.ShouldBe(expectedStatusCode);
            result.Content.ShouldNotBeNull();
            var contentAsString = result.Content.ReadAsStringAsync().Result;
            contentAsString.ShouldNotBeNull();
            contentAsString.Length.ShouldBeGreaterThan(0);
            return contentAsString;
        }

        public static ErrorsResponse ErrorsResponse(string content, int errorCount)
        {
            var errorResponse = FormatResponseMessage.DeserializeMessage<ErrorsResponse>(content);
            errorResponse.ShouldNotBeNull();
            errorResponse.Errors.Count.ShouldBe(errorCount);
            return errorResponse;
        }
    }
}