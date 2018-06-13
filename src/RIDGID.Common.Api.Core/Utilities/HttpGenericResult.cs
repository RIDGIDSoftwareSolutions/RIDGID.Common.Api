using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace RIDGID.Common.Api.Core.Utilities
{
    public class HttpGenericResult : IHttpActionResult
    {
        private readonly ApiController _controller;
        private readonly HttpStatusCode _statusCode;
        private readonly object _responseBody;

        public HttpGenericResult(ApiController controller, HttpStatusCode statusCode, object responseBody)
        {
            _controller = controller;
            _statusCode = statusCode;
            _responseBody = responseBody;
        }

        internal HttpResponseMessage Execute()
        {
            var response = new HttpResponseMessage(_statusCode);
            try
            {
                if (_responseBody != null)
                {
                    response = FormatResponseMessage.CreateMessage(_responseBody, _statusCode);
                }
                response.RequestMessage = _controller.Request;
            }
            catch (Exception)
            {
                response.Dispose();
                throw;
            }
            return response;
        }

        public Task<System.Net.Http.HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }
    }
}