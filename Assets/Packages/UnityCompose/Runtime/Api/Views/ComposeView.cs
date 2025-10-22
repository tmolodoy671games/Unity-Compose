using System;
using System.Diagnostics.CodeAnalysis;
using UnityCompose;
using UnityCompose.Packages.UnityCompose.Runtime.Impl;
using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

[SuppressMessage("ReSharper", "CheckNamespace")]
public partial class ComposeView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<ComposeView, UxmlTraits>
    {
    }

    private Action? _content;

    public void SetContent([Composable] Action content)
    {
        if (_content == content || _content?.Method == content.Method &&
            _content.Target?.GetType() == content.Target?.GetType())
            return;
        _content = content;
        userData = null;
        Clear();
        ContentImpl(content);
    }

    [Composable, Compiled]
    private void ContentImpl(Action content)
    {
        if (CurrentComposer.BeginRootComposeGroup(this)) return;
        content();
        CurrentComposer.EndComposeGroup(() => ContentImpl(content));
    }

    public string TreeStructureAsString()
    {
        if (userData == null) return "";
        return Composer.FormatTreeStructure((ComposeGroup)userData);
    }
}