using System.Linq;
using System.Net;
using System.Web.Http.Controllers;
using RIDGID.Common.Api.Core.Objects;
using RIDGID.Common.Api.Core.Utilities;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace RIDGID.Common.Api.Core.Attributes
{
    public class RidgidModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid) return;
            var errorMessages = (from key in actionContext.ModelState.Keys
                                 from error in actionContext.ModelState[key].Errors
                                 select ModelStateCustomErrorMessage.Parse(error.ErrorMessage)).ToList();
            var errorsResponseObject = new ErrorsResponse
            {
                Errors = errorMessages
            };
            actionContext.Response =
                FormatResponseMessage.CreateMessage(errorsResponseObject, HttpStatusCode.BadRequest);
        }
    }
}