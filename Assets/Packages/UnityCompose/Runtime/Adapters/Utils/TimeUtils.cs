namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Utils;

internal static class TimeUtils
{
    public static long ToLongTime(this float time)
    {
        return (long)(time * 1000);
    }
}