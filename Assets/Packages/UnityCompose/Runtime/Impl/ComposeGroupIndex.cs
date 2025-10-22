namespace UnityCompose.Packages.UnityCompose.Runtime.Impl;

internal class ComposeGroupIndex
{
    public readonly ComposeGroup Group;
    public int Index;

    public ComposeGroupIndex(ComposeGroup group)
    {
        Group = group;
    }

    public override string ToString()
    {
        return $"(Group: {Group}, Index: {Index})";
    }
}