// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Linq;
using SharpExtensions;
using StableCollections;
using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static readonly ICompositionLocal<IImmutableStableList<string?>> LocalModalMenuTags =
        CompositionLocalOf(ImmutableStableListOf<string?>);

    internal static readonly ICompositionLocal<ModalMenuManager> LocalOnScreenMenuManager =
        CompositionLocalOf<ModalMenuManager>(() =>
            throw new IllegalStateException("No LocalOnScreenManager provided!")
        );

    [Composable]
    public static void ModalMenu(
        ComposableContent content,
        string? key = null
    )
    {
        var manager = LocalOnScreenMenuManager.Current;
        DisposableEffect(content, it =>
        {
            manager.AddContent(key, content);
            return it.OnDispose(() => manager.RemoveContent(key, content));
        });
    }
}

internal class ModalMenuManager
{
    private readonly record struct ModalMenuEntry(
        string? Key,
        ComposableContent Content
    );

    private readonly IMutableStateList<ModalMenuEntry> _contents = MutableStateListOf<ModalMenuEntry>();

    public IImmutableStableList<ComposableContent> Contents => _contents
        .Select(it => it.Content)
        .ToImmutableStableList();

    public IImmutableStableList<string?> Tags => _contents
        .Select(it => it.Key)
        .ToImmutableStableList();

    public void AddContent(string? key, ComposableContent content)
    {
        _contents.Add(new ModalMenuEntry(key, content));
    }

    public void RemoveContent(string? key, ComposableContent content)
    {
        _contents.Remove(new ModalMenuEntry(key, content));
    }
}