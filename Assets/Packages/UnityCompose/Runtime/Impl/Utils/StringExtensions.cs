using System.Text;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

internal static class StringExtensions
{
    public static string Multiply(this string str, int count)
    {
        var builder = new StringBuilder();
        for (var i = 0; i < count; i++)
            builder.Append(str);
        return builder.ToString();
    }
}