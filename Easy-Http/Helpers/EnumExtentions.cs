using System;

namespace Easy_Http.Helpers
{
    public static class EnumHelpers
    {
        public static string GetEnumName<T>(this T enumValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be of type Enum");

            return Enum.GetName(typeof(T), enumValue);
        }
    }
}
