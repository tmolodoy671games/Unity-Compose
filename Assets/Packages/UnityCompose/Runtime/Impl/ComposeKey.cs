namespace UnityCompose.Packages.UnityCompose.Runtime.Impl;

internal readonly record struct ComposeKey(
    string FileName,
    string MemberName,
    int LineNumber,
    object? AdditionalKey = null
)
{
    public override string ToString()
    {
        return $"(MemberName: {MemberName})";
    }
}

internal readonly record struct ResolvedComposeKey(
    string FileName,
    string MemberName,
    int LineNumber,
    object? AdditionalKey,
    int Increment
)
{
    public static ResolvedComposeKey Create(ComposeKey key, int increment)
    {
        return new ResolvedComposeKey(
            FileName: key.FileName,
            MemberName: key.MemberName,
            LineNumber: key.LineNumber,
            AdditionalKey: key.AdditionalKey,
            Increment: increment
        );
    }
    
    public override string ToString()
    {
        return $"(MemberName: {MemberName}, Increment: {Increment})";
    }
}