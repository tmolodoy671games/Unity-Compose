namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

public interface IInteractionSource
{
    IFlow<IInteraction> Interactions { get; }
}

public interface IMutableInteractionSource : IInteractionSource
{
    void Emit(IInteraction interaction);
}

internal class MutableInteractionSourceImpl : IMutableInteractionSource
{
    private readonly MutableFlowImpl<IInteraction> _flow = new();

    public IFlow<IInteraction> Interactions => _flow;
    public void Emit(IInteraction interaction) => _flow.Emit(interaction);
}