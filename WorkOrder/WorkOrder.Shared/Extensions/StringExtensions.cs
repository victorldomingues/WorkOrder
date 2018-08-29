namespace WorkOrder.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            var pos = text.IndexOf(search);
            if (pos < 0)
                return text;
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        public static string ReplaceLast(this string text, string search, string replace)
        {
            var place = text.LastIndexOf(search);
            if (place == -1)
                return text;
            return text.Remove(place, search.Length).Insert(place, replace);
        }
    }
}
