using System;
using System.Diagnostics.CodeAnalysis;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    [Composable]
    [Compiled]
    private static void __AnimatedSize(Action content, IModifier? modifier = null, float duration = ComposeDefaults.TransitionDuration)
    {
        if (CurrentComposer.BeginComposeGroup((content, modifier, duration)))
            return;
        try
        {
            var(containerStyle, contentStyle) = AnimateSizeModifiers(duration);
            ReusableComposeView<AnimatedSize>(modifier: modifier.OrEmpty().Then(containerStyle), content: RememberComposable<global::System.Action>((content, contentStyle), () =>
            {
                CompositionLocalProvider(provides: IImmutableStableList.Create(LocalModifier.Provides(after: contentStyle)), content: content);
            }));
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __AnimatedSize(content, modifier, duration));
        }
    }
}