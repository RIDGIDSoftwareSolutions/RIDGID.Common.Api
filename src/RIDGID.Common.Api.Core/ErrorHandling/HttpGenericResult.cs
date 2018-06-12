using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using RIDGID.Common.Api.Core.Utilities;

namespace RIDGID.Common.Api.Core.ErrorHandling
{
    public class HttpGenericResult : IHttpActionResult
    {
        private readonly ApiController _controller;
        private readonly HttpStatusCode _statusCode;
        private readonly object _responseBody;
        private readonly bool _isSnakeCase;

        public HttpGenericResult(ApiController controller, HttpStatusCode statusCode, object responseBody, bool isSnakeCase = false)
        {
            _controller = controller;
            _statusCode = statusCode;
            _responseBody = responseBody;
            _isSnakeCase = isSnakeCase;
        }

        internal HttpResponseMessage Execute()
        {
            var response = new HttpResponseMessage(_statusCode);

            try
            {
                if (_responseBody != null)
                {
                    JsonMediaTypeFormatter jsonMediaTypeFormatter;
                    if (_isSnakeCase)
                    {
                        jsonMediaTypeFormatter = new JsonMediaTypeFormatter
                        {
                            SerializerSettings =
                            {
                                ContractResolver = new SnakeCasePropertyNamesContractResolver()
                            }
                        };
                    }
                    else
                    {
                        jsonMediaTypeFormatter = new JsonMediaTypeFormatter();
                    }
                    response.Content =
                        new ObjectContent(_responseBody.GetType(), _responseBody, jsonMediaTypeFormatter);
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