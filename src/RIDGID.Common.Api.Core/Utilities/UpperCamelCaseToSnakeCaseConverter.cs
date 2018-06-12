using System.Linq;
using System.Text.RegularExpressions;

namespace RIDGID.Common.Api.Core.Utilities
{
    public class UpperCamelCaseToSnakeCaseConverter
    {
        public static string ToSnakeCase(string propertyToConvert)
        {
            var startUnderscores = Regex.Match(propertyToConvert, @"^_+");
            return startUnderscores + Regex.Replace(propertyToConvert, @"([A-Z])", "_$1").ToLower().TrimStart('_');
        }

        public static string FromSnakeCase(string propertyToConvert)
        {
            var words = propertyToConvert.TrimStart('_')
                .Split('_')
                .Select(x =>
                {
                    string output = x.ToUpperInvariant()[0] + x.Substring(1);

                    return output;
                });

            return string.Join(string.Empty, words);
        }
    }
}