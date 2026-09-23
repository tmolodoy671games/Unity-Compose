// ReSharper disable CheckNamespace

using Compose.Net;
using UnityEngine.UIElements;

namespace UnityCompose;

public abstract class BaseUnityModifier<T> : BaseModifier<T> where T : BaseUnityModifier<T>
{
    public override void Apply(IReusableComposeNode node)
    {
        var element = node.VisualElement();
        Apply(element);
    }

    public override void Revert(IReusableComposeNode node)
    {
        var element = node.VisualElement();
        Apply(element);
    }
    
    public abstract void Apply(VisualElement element);
    public abstract void Revert(VisualElement element);
}