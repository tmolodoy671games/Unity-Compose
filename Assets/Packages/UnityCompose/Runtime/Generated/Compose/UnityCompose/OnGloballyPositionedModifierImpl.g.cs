// ReSharper disable CheckNamespace
using System;
using System.Collections.Generic;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose;
internal partial class OnGloballyPositionedModifierImpl
{
    [Composable, DontGenerateComposeGroups]
    [Compiled]
    private void __Apply(VisualElement element)
    {
        DisposableEffect(key: element, effect: Remember<global::System.Func<global::UnityCompose.IDisposableEffectScope, global::System.IDisposable>>((this, element), it =>
        {
            var ancestors = element.Ancestors().ToImmutableStableList();
            foreach (var ancestor in ancestors)
                ancestor.GetComposeCallback<GeometryChangedEvent>().Add(_onGeometryChanged);
            return it.OnDispose(() =>
            {
                foreach (var ancestor in ancestors)
                    ancestor.GetComposeCallback<GeometryChangedEvent>().Remove(_onGeometryChanged);
            });
        }));
    }
}