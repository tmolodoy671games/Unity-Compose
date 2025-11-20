// ReSharper disable CheckNamespace

namespace UnityCompose;

internal class ComposeObjectKey
{
    public static readonly ComposeObjectKey None = new();

    private ComposeObjectKey()
    {
    }

    public override string ToString() => "None";
}

public class ComposeRememberKey
{
    public static readonly ComposeRememberKey None = new();

    private ComposeRememberKey()
    {
    }

    public override string ToString() => "None";
}