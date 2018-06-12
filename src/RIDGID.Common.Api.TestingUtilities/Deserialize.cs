using Newtonsoft.Json;
using RIDGID.Common.Api.Core.Objects;
using RIDGID.Common.Api.Core.Utilities;
using Shouldly;

namespace RIDGID.Common.Api.TestingUtilities
{
    public class Deserialize
    {
        public static ErrorsResponse DeserializeErrorResponse(string content, int errorCount)
        {
            var errorResponse = JsonConvert.DeserializeObject<ErrorsResponse>(content,
                new JsonSerializerSettings { ContractResolver = new SnakeCasePropertyNamesContractResolver() });
            errorResponse.ShouldNotBeNull();
            errorResponse.Errors.Count.ShouldBe(errorCount);
            return errorResponse;
        }
    }
}