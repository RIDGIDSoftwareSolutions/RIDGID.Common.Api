using Newtonsoft.Json;
using RIDGID.Common.Api.Core.Objects;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
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
            this.ErrorId = errorId;
        }

        public RidgidAuthorizeAttribute()
        {
            this.ErrorId = 1;
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

            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Content = new StringContent(JsonConvert.SerializeObject(errorMessages), Encoding.UTF8,
                    "application/json")
            };
        }
    }
}