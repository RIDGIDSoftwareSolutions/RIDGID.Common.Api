namespace RIDGID.Common.Api.Core.Utilities
{
    public class CamelCaseToSnakeCaseConverter
    {
        public static string ToSnakeCase(string propertyToConvert)
        {
            propertyToConvert = propertyToConvert.ToUpperInvariant()[0] + propertyToConvert.Substring(1);
            return UpperCamelCaseToSnakeCaseConverter.ToSnakeCase(propertyToConvert);
        }

        public static string FromSnakeCase(string propertyToConvert)
        {
            var result = UpperCamelCaseToSnakeCaseConverter.FromSnakeCase(propertyToConvert);
            return result.ToLowerInvariant()[0] + result.Substring(1);
        }
    }
}