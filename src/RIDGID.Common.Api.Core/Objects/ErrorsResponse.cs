using System.Collections.Generic;

namespace RIDGID.Common.Api.Core.Objects
{
    public class ErrorsResponse
    {
        public List<ErrorMessage> Errors { get; set; }

        public ErrorsResponse()
        {

        }

        public ErrorsResponse(string debugErrorMessage, int errorId)
        {
            Errors = new List<ErrorMessage>
            {
                new ErrorMessage
                {
                    DebugErrorMessage = debugErrorMessage,
                    ErrorId = errorId
                }
            };
        }
    }
}