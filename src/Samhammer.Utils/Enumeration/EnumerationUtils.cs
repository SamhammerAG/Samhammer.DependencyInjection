using System;

namespace Samhammer.Utils.Enumeration
{
    public class EnumerationUtils
    {
        public static T Parse<T>(string value, T defaultValue = default(T)) where T : struct
        {
            if (!Enum.TryParse(value, true, out T result))
            {
                result = defaultValue;
            }

            return result;
        }

        public static bool IsDefined<T>(string value) where T : struct
        {
            var isDefined = Enum.TryParse(value, true, out T _);
            return isDefined;
        }
    }
}
