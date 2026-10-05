// ReSharper disable CheckNamespace

using System;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal class GenerateVisualContent
{
    private readonly IMutableStableList<Action<MeshGenerationContext>> _generateVisualContent =
        MutableStableListOf<Action<MeshGenerationContext>>();

    public void Insert(int index, Action<MeshGenerationContext> generateVisualContent)
    {
        index = Math.Min(index, _generateVisualContent.Count);
        _generateVisualContent.Insert(index, generateVisualContent);
    }

    public void Remove(Action<MeshGenerationContext> generateVisualContent)
    {
        _generateVisualContent.Remove(generateVisualContent);
    }

    internal void Invoke(MeshGenerationContext context)
    {
        foreach (var generateVisualContent in _generateVisualContent)
            generateVisualContent(context);
    }
}

public static partial class VisualElementExtensions
{
    internal static GenerateVisualContent GenerateVisualContent(this VisualElement visualElement)
    {
        const string key = "UnityCompose_GenerateVisualElement";
        if (visualElement.UserData().TryGet(key, out var cached))
            return (GenerateVisualContent)cached.NotNull();
        var newInstance = new GenerateVisualContent();
        visualElement.UserData()[key] = newInstance;
        visualElement.generateVisualContent = newInstance.Invoke;
        return newInstance;
    }
}