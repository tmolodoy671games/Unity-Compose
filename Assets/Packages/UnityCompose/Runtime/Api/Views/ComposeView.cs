using System.Diagnostics.CodeAnalysis;
using UnityCompose;
using UnityEngine.UIElements;

[SuppressMessage("ReSharper", "CheckNamespace")]
public partial class ComposeView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<ComposeView, UxmlTraits>
    {
    }

    private readonly Composer _composer = new();
    private ComposableContent? _content;

    public void SetContent(ComposableContent content)
    {
        if (_content == content)
            return;
        _content = content;
        userData = null;
        _composer.SetAsCurrentComposer();
        Clear();
        ContentImpl(content);
        _composer.ResetAsCurrentComposer();
    }

    [Composable]
    private void ContentImpl(ComposableContent content)
    {
        var composer = CurrentComposer;
        composer.StartReusableGroup(0);
        composer.SetVisualElement(this);
        composer.EnterVisualElement(this);
        CompositionLocalProvider(
            LocalVisualElement.Provides(this),
            content
        );
        composer.EndReusableGroup(0);
    }

    public override string ToString() => "ComposeView";
}