using System.Diagnostics.CodeAnalysis;
using UnityCompose;
using UnityEngine.UIElements;

[SuppressMessage("ReSharper", "CheckNamespace")]
public partial class ComposeView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<ComposeView, UxmlTraits>
    {
    }

    private ComposableContent? _content;

    public void SetContent(ComposableContent content)
    {
        if (_content == content)
            return;
        _content = content;
        userData = null;
        Clear();
        // CurrentComposer.Reset();
        ContentImpl(content);
    }

    [Composable, Compiled]
    private void ContentImpl(ComposableContent content)
    {
        CurrentComposer.StartReusableGroup(0);
        CurrentComposer.SetVisualElement(this);
        CurrentComposer.EnterVisualElement();
        CompositionLocalProvider(
            LocalVisualElement.Provides(this),
            LocalLayoutMeasurer.Provides(new LayoutMeasurerImpl(this)),
            content: content
        );
        CurrentComposer.EndReusableGroup(0);
    }

    public override string ToString() => "ComposeView";
}