// ReSharper disable CheckNamespace

using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public interface IDrawBehindModifier : IModifier
{
}

public abstract class DrawBehindUnityModifier<T> : UnityModifier<T>,
    IDrawBehindModifier where T : DrawBehindUnityModifier<T>
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
            drawOn: node.SetupDrawBehind(),
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
            drawOn: node.SetupDrawBehind(),
            newModifiers: newModifiers
        );
        foreach (var newModifier in newModifiers)
        {
            if (newModifier is IDrawBehindModifier)
                return;
        }
        node.RemoveDrawBehind();
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