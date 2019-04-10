using System.Linq;

namespace Samhammer.Utils.String
{
    public static class StringExtensions
    {
        public static string RemoveString(this string value, string removeValue)
        {
            return value?.Replace(removeValue, string.Empty);
        }

        public static string ToLowerFirstChar(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return value.First().ToString().ToLower() + value.Substring(1);
        }
    }
}
