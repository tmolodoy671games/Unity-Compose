namespace UnityCompose.Packages.UnityCompose.Runtime.Impl;

internal class ComposeInvocationState
{
    public static readonly ComposeInvocationState Empty = new();
        
    public int InvocationCount;
}