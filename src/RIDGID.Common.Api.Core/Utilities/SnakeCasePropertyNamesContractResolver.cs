using Newtonsoft.Json.Serialization;

namespace RIDGID.Common.Api.Core.Utilities
{
    public class SnakeCasePropertyNamesContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return UpperCamelCaseToSnakeCaseConverter.ToSnakeCase(propertyName);
        }
    }
}