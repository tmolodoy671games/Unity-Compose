namespace Packages.UnityCompose.Impl.Composition.Utils;

internal static class ComposeGroupKey
{
    public static int Get(string filePath, int lineNumber)
    {
        if (filePath.Length == 0)
            return lineNumber;
        return filePath.GetHashCode() + lineNumber;
    }
}