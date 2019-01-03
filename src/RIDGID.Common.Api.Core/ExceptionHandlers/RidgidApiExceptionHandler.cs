using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using RIDGID.Common.Api.Core.Utilities;

namespace RIDGID.Common.Api.Core.ExceptionHandlers
{
    public class RidgidApiExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var apiController = context.ExceptionContext.ControllerContext.Controller as ApiController;

            context.Result = FormatResponseMessage.CreateErrorResponse(apiController, 1,
                "An internal sever error occurred. This is not your fault.",
                HttpStatusCode.InternalServerError);
        }

        public override Boolean ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }
    }
}
