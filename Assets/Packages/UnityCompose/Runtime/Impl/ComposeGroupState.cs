namespace UnityCompose.Packages.UnityCompose.Runtime.Impl;

internal class ComposeGroupState
{
    public readonly ComposeGroup ComposeGroup;
    public bool InvokedThisStep = false;

    public ComposeGroupState(ComposeGroup composeGroup)
    {
        ComposeGroup = composeGroup;
    }
}