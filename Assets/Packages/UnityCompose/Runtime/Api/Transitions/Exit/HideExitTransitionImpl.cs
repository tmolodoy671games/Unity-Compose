// ReSharper disable CheckNamespace

namespace UnityCompose;

internal class HideExitTransitionImpl : IExitTransition
{
    public static readonly HideExitTransitionImpl Instance = new();
    
    private HideExitTransitionImpl() {}
    
    public IModifier Get(float progress, LayoutInfo parent)
    {
        return Modifier
            .Alpha(0f);
    }
}