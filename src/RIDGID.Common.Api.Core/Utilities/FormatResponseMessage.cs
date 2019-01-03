using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using RIDGID.Common.Api.Core.Objects;

namespace RIDGID.Common.Api.Core.Utilities
{
    public class FormatResponseMessage
    {
        public static string CreateJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, JsonSerializerSetting());
        }

        public static object GetObject(string json)
        {
            return JsonConvert.DeserializeObject(json, JsonSerializerSetting());
        }

        public static IHttpActionResult CreateErrorResponse(ApiController apiControllerThatGeneratedError, int errorId, string debugErrorMessage, HttpStatusCode httpStatusCode)
        {
            var errors = new List<ErrorMessage>
            {
                new ErrorMessage
                {
                    DebugErrorMessage = debugErrorMessage,
                    ErrorId = errorId
                }
            };
            var errorsResponse = new ErrorsResponse
            {
                Errors = errors
            };
            return new HttpGenericResult(apiControllerThatGeneratedError, httpStatusCode, errorsResponse);
        }

        public static HttpResponseMessage CreateMessage(object responseBody, HttpStatusCode statusCode)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = statusCode
            };

            if (responseBody != null)
            {
                response.Content = new StringContent(CreateJson(responseBody), Encoding.UTF8, "application/json");
            }
            return response;
        }

        public static bool IsSnakeCase()
        {
            if (!bool.TryParse(ConfigurationManager.AppSettings["snakecase"], out var setting))
            {
                setting = false;
            }
            return setting;
        }

        public static JsonSerializerSettings JsonSerializerSetting()
        {
            return IsSnakeCase()
                ? new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy(true, true) }
                }
                : new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy(true, true) },
                };
        }

        public static string ConvertCamelCaseIntoSnakeCase(string str)
        {
            var charList = new List<char>();
            for (var i = 0; i < str.Length; i++)
            {
                if (char.IsUpper(str[i]))
                {
                    if (i != 0)
                    {
                        charList.Add('_');
                    }
                    charList.Add(char.ToLower(str[i]));
                }
                else
                {
                    charList.Add(str[i]);
                }
            }

            var snakeCaseString = "";
            foreach (var c in charList)
            {
                snakeCaseString += c;
            }
            return snakeCaseString;
        }

        public static void SetSnakeCaseSetting(bool value)
        {
            var valueAsString = "";
            if (value)
            {
                valueAsString = "true";
            }
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["snakecase"].Value = valueAsString;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static string GetCasing(string str)
        {
            return IsSnakeCase() ? ConvertCamelCaseIntoSnakeCase(str) : str;
        }
    }
}