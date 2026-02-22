using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl;

internal class ComposeGenerateVisualContent
{
    private readonly List<Action<MeshGenerationContext>> _draws = new(2);

    public ComposeGenerateVisualContent(VisualElement visualElement)
    {
        _draws.Add(visualElement.generateVisualContent);
        visualElement.generateVisualContent = GenerateVisualContent;
    }

    public void GenerateVisualContent(MeshGenerationContext context)
    {
        foreach (var draw in _draws)
            draw?.Invoke(context);
    }
    
    public void AddBefore(Action<MeshGenerationContext> draw) => _draws.Insert(0, draw);
    public void AddAfter(Action<MeshGenerationContext> draw) => _draws.Add(draw);
    public void Remove(Action<MeshGenerationContext> draw) => _draws.Remove(draw);
}

internal static partial class VisualElementExtensions
{
    public static ComposeGenerateVisualContent ComposeGenerateVisualContent(this VisualElement visualElement)
    {
        const string key = "UnityCompose.ComposeGenerateVisualContent";
        var userData = visualElement.UserData();
        if (userData.TryGet(key, out var cachedInstance))
            return (ComposeGenerateVisualContent)cachedInstance!;
        var newInstance = new ComposeGenerateVisualContent(visualElement);
        userData[key] = newInstance;
        return newInstance;
    }
}