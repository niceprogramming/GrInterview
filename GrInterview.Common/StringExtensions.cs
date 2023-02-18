namespace GrInterview.Common
{
    public static class StringExtensions
    {
        public static string? FindDelimiter(this string input, string[] delimiters)
        {
            return delimiters.FirstOrDefault(x => input.Contains(x));
        }
    }
}
