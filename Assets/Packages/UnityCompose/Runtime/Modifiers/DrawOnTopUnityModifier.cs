// ReSharper disable CheckNamespace

using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public interface IDrawOnModifier : IModifier
{
}

public abstract class DrawOnTopUnityModifier<T> : UnityModifier<T>,
    IDrawOnModifier where T : DrawOnTopUnityModifier<T>
{
    protected sealed override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        Apply(
            node: node,
            element: element,
            drawOn: node.SetupDrawOnTop(),
            newModifiers: newModifiers
        );
    }

    protected sealed override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        Revert(
            node: node,
            element: element,
            drawOn: node.SetupDrawOnTop(),
            newModifiers: newModifiers
        );
        foreach (var newModifier in newModifiers)
        {
            if (newModifier is IDrawOnModifier)
                return;
        }
        node.RemoveDrawOnTop();
    }

    protected abstract void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        VisualElement drawOn,
        IStableList<IModifier> newModifiers
    );

    protected abstract void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        VisualElement drawOn,
        IStableList<IModifier> newModifiers
    );
}