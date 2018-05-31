namespace Faez.BudgetAssistant.Core.Extensions
{
    using System;

    public static class ArgumentExtensions
    {
        public static T EnsureArgumentNotNull<T>(this T value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }

            return value;
        }

        public static string EnsureArgumentNotNullOrWhitespace(this string value, string name)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(name, "Cannot be null or whitespace");
            }

            return value;
        }

        public static string EnsureArgumentIsCurrency(this string value, string name)
        {
            EnsureArgumentNotNullOrWhitespace(value, nameof(value));

            if (value.Length != 3)
            {
                throw new ArgumentException("Must be a 3-digit currency code");
            }

            return value;
        }
    }
}