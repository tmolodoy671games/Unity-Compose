// ReSharper disable CheckNamespace

using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public abstract class UnityModifier<T> : BaseModifier<T> where T : UnityModifier<T>
{
    public sealed override void Apply(IReusableComposeNode node, IStableList<IModifier> newModifiers)
    {
        var unityNode = (UnityReusableComposeNode)node;
        Apply(
            node: unityNode,
            element: unityNode.VisualElement,
            newModifiers: newModifiers
        );
    }

    public sealed override void Revert(IReusableComposeNode node, IStableList<IModifier> newModifiers)
    {
        var unityNode = (UnityReusableComposeNode)node;
        Revert(
            node: unityNode,
            element: unityNode.VisualElement,
            newModifiers: newModifiers
        );
    }

    public sealed override void Flatten(IMutableStableCollection<IModifier> modifiers)
    {
        base.Flatten(modifiers);
    }

    protected abstract void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    );

    protected abstract void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    );
}

public interface IAppearanceModifier : IModifier
{
}