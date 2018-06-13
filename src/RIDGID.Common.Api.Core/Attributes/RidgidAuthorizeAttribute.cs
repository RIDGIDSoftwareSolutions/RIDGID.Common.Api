using RIDGID.Common.Api.Core.Objects;
using RIDGID.Common.Api.Core.Utilities;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace RIDGID.Common.Api.Core.Attributes
{
    public class RidgidAuthorizeAttribute : AuthorizeAttribute
    {
        private const string UNAUTHORIZED_MESSAGE = "Unauthorized.";

        public int ErrorId { get; set; }

        public RidgidAuthorizeAttribute(int errorId)
        {
            ErrorId = errorId;
        }

        public RidgidAuthorizeAttribute()
        {
            ErrorId = 1;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var errorMessages = new List<ErrorMessage>
            {
                new ErrorMessage
                {
                    DebugErrorMessage = UNAUTHORIZED_MESSAGE,
                    ErrorId = this.ErrorId
                }
            };
            actionContext.Response = FormatResponseMessage.CreateMessage(errorMessages, HttpStatusCode.Unauthorized);
        }
    }
}