using RIDGID.Common.Api.Core.Objects;
using RIDGID.Common.Api.Core.Utilities;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace RIDGID.Common.Api.Core
{
    public class RidgidApiController : ApiController
    {
        // Note just some responses are in here, if you need one that is not listed use the HttpGenericErrorResponse
        internal IHttpActionResult HttpGenericErrorResponse(int errorId, string debugErrorMessage,
            HttpStatusCode httpStatusCode)
        {
            return FormatResponseMessage.CreateErrorResponse(this, errorId, debugErrorMessage, httpStatusCode);
        }

        internal virtual IHttpActionResult NoContent()
        {
            return new HttpGenericResult(this, HttpStatusCode.NoContent, null);
        }

        internal virtual IHttpActionResult BadRequest(int errorId, string debugErrorMessage)
        {
            return HttpGenericErrorResponse(errorId, debugErrorMessage, HttpStatusCode.BadRequest);
        }

        internal virtual IHttpActionResult Conflict(int errorId, string debugErrorMessage)
        {
            return HttpGenericErrorResponse(errorId, debugErrorMessage, HttpStatusCode.Conflict);
        }

        internal virtual IHttpActionResult NotFound(int errorId, string debugErrorMessage)
        {
            return HttpGenericErrorResponse(errorId, debugErrorMessage, HttpStatusCode.NotFound);
        }

        internal virtual IHttpActionResult Forbidden(int errorId, string debugErrorMessage)
        {
            return HttpGenericErrorResponse(errorId, debugErrorMessage, HttpStatusCode.Forbidden);
        }

        internal virtual IHttpActionResult Unauthorized(int errorId, string debugErrorMessage)
        {
            return HttpGenericErrorResponse(errorId, debugErrorMessage, HttpStatusCode.Unauthorized);
        }

        internal virtual IHttpActionResult InternalServerError(int errorId, string debugErrorMessage)
        {
            return HttpGenericErrorResponse(errorId, debugErrorMessage, HttpStatusCode.InternalServerError);
        }
    }
}