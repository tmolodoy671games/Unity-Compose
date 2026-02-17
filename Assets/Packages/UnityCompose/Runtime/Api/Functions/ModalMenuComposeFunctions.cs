// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using SharpExtensions;
using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    internal static readonly ICompositionLocal<ModalMenuManager> LocalOnScreenMenuManager =
        CompositionLocalOf<ModalMenuManager>(() =>
            throw new IllegalStateException("No LocalOnScreenManager provided!")
        );

    [Composable]
    public static void ModalMenu(
        ComposableContent content
    )
    {
        var manager = LocalOnScreenMenuManager.Current;
        DisposableEffect(content, it =>
        {
            manager.AddContent(content);
            return it.OnDispose(() => manager.RemoveContent(content));
        });
    }
}

internal class ModalMenuManager
{
    private readonly IMutableStateList<ComposableContent> _contents = MutableStateListOf<ComposableContent>();

    public IStateList<ComposableContent> Contents => _contents;

    public void AddContent(ComposableContent content)
    {
        _contents.Add(content);
    }

    public void RemoveContent(ComposableContent content)
    {
        _contents.Remove(content);
    }
}