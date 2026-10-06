// ReSharper disable CheckNamespace

using Compose.Net;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public interface IBackgroundShadowModifier : IModifier
{
}

public abstract class BackgroundShadowUnityModifier<T> : UnityModifier<T>,
    IBackgroundShadowModifier where T : BackgroundShadowUnityModifier<T>
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
            shadow: node.SetupBackgroundShadow(),
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
            shadow: node.SetupBackgroundShadow(),
            newModifiers: newModifiers
        );
        foreach (var newModifier in newModifiers)
        {
            if (newModifier is IBackgroundShadowModifier)
                return;
        }
        node.RemoveBackgroundShadow();
    }

    protected abstract void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        VisualElement shadow,
        IStableList<IModifier> newModifiers
    );

    protected abstract void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        VisualElement shadow,
        IStableList<IModifier> newModifiers
    );
}