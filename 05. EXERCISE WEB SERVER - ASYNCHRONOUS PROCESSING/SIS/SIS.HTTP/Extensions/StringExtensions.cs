namespace SIS.HTTP.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string word)
            => word = char.ToUpper(word[0]) + word.Substring(1).ToLower();
    }
}
