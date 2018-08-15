using RIDGID.Common.Api.Core.Objects;
using RIDGID.Common.Api.Core.Utilities;
using System.Linq;
using System.Net;
using System.Web.Http.Controllers;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace RIDGID.Common.Api.Core.Attributes
{
    public class RidgidValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid) return;
            var errorMessages = (from key in actionContext.ModelState.Keys
                                 from error in actionContext.ModelState[key].Errors
                                 select ModelStateCustomErrorMessage.Parse(error.ErrorMessage))
                                 .GroupBy(x => new { x.ErrorId, x.DebugErrorMessage })
                                 .Select(x => x.First())
                                 .ToList();
            var errorsResponseObject = new ErrorsResponse
            {
                Errors = errorMessages
            };
            actionContext.Response =
                FormatResponseMessage.CreateMessage(errorsResponseObject, HttpStatusCode.BadRequest);
        }
    }
}