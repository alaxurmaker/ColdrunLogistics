using System.Text.RegularExpressions;

namespace ColdrunLogistics.Models.Extensions.StringExtensions
{
    public static class StringExtensions
    {
        public static bool IsAlphanumeric(this string input) => Regex.IsMatch(input, "^[a-zA-Z0-9]+$");
    }
}
