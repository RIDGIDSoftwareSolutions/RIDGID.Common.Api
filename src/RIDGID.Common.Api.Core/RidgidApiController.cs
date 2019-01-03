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
        [NonAction]
        public IHttpActionResult HttpGenericErrorResponse(int errorId, string debugErrorMessage,
            HttpStatusCode httpStatusCode)
        {
            return FormatResponseMessage.CreateErrorResponse(this, errorId, debugErrorMessage, httpStatusCode);
        }

        [NonAction]
        public virtual IHttpActionResult NoContent()
        {
            return new HttpGenericResult(this, HttpStatusCode.NoContent, null);
        }

        [NonAction]
        public virtual IHttpActionResult BadRequest(int errorId, string debugErrorMessage)
        {
            return HttpGenericErrorResponse(errorId, debugErrorMessage, HttpStatusCode.BadRequest);
        }

        [NonAction]
        public virtual IHttpActionResult Conflict(int errorId, string debugErrorMessage)
        {
            return HttpGenericErrorResponse(errorId, debugErrorMessage, HttpStatusCode.Conflict);
        }

        [NonAction]
        public virtual IHttpActionResult NotFound(int errorId, string debugErrorMessage)
        {
            return HttpGenericErrorResponse(errorId, debugErrorMessage, HttpStatusCode.NotFound);
        }

        [NonAction]
        public virtual IHttpActionResult Forbidden(int errorId, string debugErrorMessage)
        {
            return HttpGenericErrorResponse(errorId, debugErrorMessage, HttpStatusCode.Forbidden);
        }

        [NonAction]
        public virtual IHttpActionResult Unauthorized(int errorId, string debugErrorMessage)
        {
            return HttpGenericErrorResponse(errorId, debugErrorMessage, HttpStatusCode.Unauthorized);
        }

        [NonAction]
        public virtual IHttpActionResult InternalServerError(int errorId, string debugErrorMessage)
        {
            return HttpGenericErrorResponse(errorId, debugErrorMessage, HttpStatusCode.InternalServerError);
        }
    }
}