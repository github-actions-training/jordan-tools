using System.Text;

namespace Jordan.Tools.Strings
{
    public static class StringExtensions
    {
        public static string? Base64Encode(this string str, Encoding? encoding = null)
        {
            if (str is null)
                return null;

            if (encoding == null)
                encoding = Encoding.UTF8;

            var bytes = encoding.GetBytes(str);
            var b64string = Convert.ToBase64String(bytes);
            return b64string;
        }

        public static string? Base64Decode(this string str, Encoding? encoding = null)
        {
            if (str is null)
                return null;

            if (encoding == null)
                encoding = Encoding.UTF8;

            var bytes = Convert.FromBase64String(str);
            return encoding.GetString(bytes);
        }
    }
}