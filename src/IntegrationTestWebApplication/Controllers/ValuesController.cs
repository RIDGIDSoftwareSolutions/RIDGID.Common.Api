using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RIDGID.Common.Api.Core;
using RIDGID.Common.Api.Core.Attributes;

namespace IntegrationTestWebApplication.Controllers
{
    public class ValuesController : RidgidApiController
    {
        [HttpGet]
        public IHttpActionResult UnhandledException()
        {
            throw new Exception("An unhandled exception occurred!");
        }

        
        [HttpGet]
        [RidgidValidateModel]
        public IHttpActionResult ResourceWithRequiredFields([FromUri] ModelWithRequiredAttribute model)
        {
            return Ok();
        }
    }

    public class ModelWithRequiredAttribute
    {
        [RidgidRequired(1)]
        public string RequiredFieldWithEmptyAllowed { get; set; }

        [RidgidRequired(2)]
        public string RequiredFieldWithEmptyNotAllowed { get; set; }
    }
}
