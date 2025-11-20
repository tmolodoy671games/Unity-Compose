namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;

internal class ComposeGroupState<T>
{
    public T Value { get; set; }

    public ComposeGroupState(T value)
    {
        Value = value;
    }
}