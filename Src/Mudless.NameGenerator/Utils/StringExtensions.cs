namespace Mudless.NameGenerator.Utils
{
    internal static class StringExtensions
    {
        public static string UpperFirst(this string name)
        {
            return name[0].ToString().ToUpperInvariant() + name.Substring(1);
        }
    }
}