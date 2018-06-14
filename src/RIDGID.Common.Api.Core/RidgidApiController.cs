using RIDGID.Common.Api.Core.Objects;
using RIDGID.Common.Api.Core.Utilities;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace RIDGID.Common.Api.Core
{
    public class RidgidApiController : ApiController
    {
        public IHttpActionResult HttpGenericErrorResponse(int errorId, string debugErrorMessage,
            HttpStatusCode httpStatusCode)
        {
            var errors = new List<ErrorMessage>
            {
                new ErrorMessage
                {
                    DebugErrorMessage = debugErrorMessage,
                    ErrorId = errorId
                }
            };
            var errorsResponse = new ErrorsResponse
            {
                Errors = errors
            };
            return new HttpGenericResult(this, httpStatusCode, errorsResponse);
        }

        public virtual IHttpActionResult NoContent()
        {
            return new HttpGenericResult(this, HttpStatusCode.NoContent, null);
        }

        public virtual IHttpActionResult BadRequest(int errorId, string debugErrorMessage)
        {
            return HttpGenericErrorResponse(errorId, debugErrorMessage, HttpStatusCode.BadRequest);
        }

        public virtual IHttpActionResult Conflict(int errorId, string debugErrorMessage)
        {
            return HttpGenericErrorResponse(errorId, debugErrorMessage, HttpStatusCode.Conflict);
        }

        public virtual IHttpActionResult NotFound(int errorId, string debugErrorMessage)
        {
            return HttpGenericErrorResponse(errorId, debugErrorMessage, HttpStatusCode.NotFound);
        }

        public virtual IHttpActionResult Forbidden(int errorId, string debugErrorMessage)
        {
            return HttpGenericErrorResponse(errorId, debugErrorMessage, HttpStatusCode.Forbidden);
        }

        public virtual IHttpActionResult Unauthorized(int errorId, string debugErrorMessage)
        {
            return HttpGenericErrorResponse(errorId, debugErrorMessage, HttpStatusCode.Unauthorized);
        }

        public virtual IHttpActionResult InternalServerError(int errorId, string debugErrorMessage)
        {
            return HttpGenericErrorResponse(errorId, debugErrorMessage, HttpStatusCode.InternalServerError);
        }
    }
}