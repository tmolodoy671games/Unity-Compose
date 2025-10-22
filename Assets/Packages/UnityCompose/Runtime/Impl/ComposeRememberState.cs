namespace UnityCompose.Packages.UnityCompose.Runtime.Impl;

internal class ComposeRememberState
{
    public bool InvokedThisStep;
    public object Key;
    public object? Value;

    public ComposeRememberState(object key, object? value)
    {
        Key = key;
        Value = value;
        InvokedThisStep = true;
    }
}