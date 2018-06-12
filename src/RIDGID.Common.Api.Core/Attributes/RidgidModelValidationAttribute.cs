using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RIDGID.Common.Api.Core.Objects;
using RIDGID.Common.Api.Core.Utilities;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace RIDGID.Common.Api.Core.Attributes
{
    public class RidgidModelValidationAttribute : ActionFilterAttribute
    {
        private readonly bool _isSnakeCase;

        public RidgidModelValidationAttribute(bool isSnakeCase = false)
        {
            _isSnakeCase = isSnakeCase;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid) return;
            var errorMessages = (from key in actionContext.ModelState.Keys
                                 from error in actionContext.ModelState[key].Errors
                                 select ModelValidationUtilities.ParseModelStateErrorMessage(error.ErrorMessage)).ToList();
            var errorsResponseObject = new ErrorsResponse
            {
                Errors = errorMessages
            };
            string json;
            ParseAppConfig.DetermineIfSnakeCase();
            if (_isSnakeCase)
            {
                json = JsonConvert.SerializeObject(errorsResponseObject, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    },
                    Formatting = Formatting.Indented
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(errorsResponseObject);
            }
            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
        }
    }
}