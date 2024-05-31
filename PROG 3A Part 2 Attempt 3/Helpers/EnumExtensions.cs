using System.Text;
using System.Text.RegularExpressions;

namespace PROG_3A_Part_2_Attempt_3.Helpers
{
    /// <summary>
    /// Provides extension methods for enum types.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Converts the enum value to a category display name.
        /// </summary>
        /// <param name="enumValue">The enum value to convert.</param>
        /// <returns>The category display name.</returns>
        public static string ToCategoryDisplayName(this Enum enumValue)
        {
            var value = enumValue.ToString();
            var result = new StringBuilder(value.Length * 2);
            result.Append(value[0]);

            for (int i = 1; i < value.Length; i++)
            {
                if (char.IsUpper(value[i]))
                {
                    result.Append(' ');
                }
                result.Append(value[i]);
            }

            return result.ToString();
        }
    }
}
