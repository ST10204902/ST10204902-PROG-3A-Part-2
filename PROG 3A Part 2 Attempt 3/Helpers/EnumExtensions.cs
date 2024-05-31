using System.Text.RegularExpressions;

namespace PROG_3A_Part_2_Attempt_3.Helpers
{
    public static class EnumExtensions
    {
        public static string ToCategoryDisplayName(this Enum enumValue)
        {
            return Regex.Replace(enumValue.ToString(), "(\\B[A-Z])", " $1");
        }
    }
}
