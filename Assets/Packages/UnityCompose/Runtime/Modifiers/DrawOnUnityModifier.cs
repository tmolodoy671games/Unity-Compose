// ReSharper disable CheckNamespace

using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public interface IDrawOnModifier : IModifier
{
}

public abstract class DrawOnUnityModifier<T> : UnityModifier<T>,
    IDrawOnModifier where T : DrawOnUnityModifier<T>
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
            drawOn: node.SetupDrawOn(),
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
            drawOn: node.SetupDrawOn(),
            newModifiers: newModifiers
        );
        foreach (var newModifier in newModifiers)
        {
            if (newModifier is IDrawOnModifier)
                return;
        }
        node.RemoveDrawOn();
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