namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

public interface IInteractionSource
{
    IFlow<IInteraction> Interactions { get; }
}

public static partial class InteractionSourceExtensions
{
    [Composable]
    public static IState<bool> CollectIsHoveredAsState(this IInteractionSource interactionSource)
    {
        var isHovered = Remember(() => MutableStateOf(false));
        DisposableEffect((interactionSource, isHovered), scope =>
        {
            var disposable = interactionSource.Interactions
                .Collect(it => isHovered.Value = it switch
                {
                    IHoverInteraction.Enter => true,
                    IHoverInteraction.Exit => false,
                    _ => isHovered.Value
                });
            return scope.OnDispose(disposable.Dispose);
        });
        return isHovered;
    }
    
    [Composable]
    public static IState<bool> CollectIsFocusedAsState(this IInteractionSource interactionSource)
    {
        var isFocused = Remember(() => MutableStateOf(false));
        DisposableEffect((interactionSource, isFocused), scope =>
        {
            var disposable = interactionSource.Interactions
                .Collect(it => isFocused.Value = it switch
                {
                    IFocusInteraction.Focus => true,
                    IFocusInteraction.Unfocus => false,
                    _ => isFocused.Value
                });
            return scope.OnDispose(disposable.Dispose);
        });
        return isFocused;
    }
    
    [Composable]
    public static IState<bool> CollectIsPressedAsState(this IInteractionSource interactionSource)
    {
        var isPressed = Remember(() => MutableStateOf(false));
        DisposableEffect((interactionSource, isPressed), scope =>
        {
            var disposable = interactionSource.Interactions
                .Collect(it => isPressed.Value = it switch
                {
                    IPressInteraction.Press => true,
                    IPressInteraction.Release => false,
                    IPressInteraction.Cancel => false,
                    _ => isPressed.Value
                });
            return scope.OnDispose(disposable.Dispose);
        });
        return isPressed;
    }
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