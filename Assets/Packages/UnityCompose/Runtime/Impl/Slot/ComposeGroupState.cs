namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;

internal interface IComposeGroupState {}

internal class ComposeGroupState<T> : IComposeGroupState
{
    public T Value;

    public ComposeGroupState(T value)
    {
        Value = value;
    }
}