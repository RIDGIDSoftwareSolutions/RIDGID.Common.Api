using System.Net;
using System.Threading;
using System.Web.Http;
using Shouldly;

namespace RIDGID.Common.Api.TestingUtilities
{
    public class ResponseContent
    {
        public static string GetContentAsString(IHttpActionResult actionResult, HttpStatusCode expectedStatusCode)
        {
            var result = actionResult.ExecuteAsync(new CancellationToken()).Result;
            result.StatusCode.ShouldBe(expectedStatusCode);
            result.Content.ShouldNotBeNull();
            var contentAsString = result.Content.ReadAsStringAsync().Result;
            contentAsString.ShouldNotBeNull();
            contentAsString.Length.ShouldBeGreaterThan(0);
            return contentAsString;
        }
    }
}