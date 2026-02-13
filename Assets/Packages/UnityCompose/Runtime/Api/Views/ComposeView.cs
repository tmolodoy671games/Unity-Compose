using System.Diagnostics.CodeAnalysis;
using UnityCompose;
using UnityEngine.UIElements;

[SuppressMessage("ReSharper", "CheckNamespace")]
public partial class ComposeView : VisualElement
{
#pragma warning disable CS0618 // Type or member is obsolete
    public new class UxmlFactory : UxmlFactory<ComposeView, UxmlTraits>
#pragma warning restore CS0618 // Type or member is obsolete
    {
    }

    private readonly Composer _composer = new();
    private ComposableContent<Composer, int>? _content;

    public void SetContent(ComposableContent<Composer, int> content)
    {
        pickingMode = PickingMode.Ignore;
        if (_content == content)
            return;
        _content = content;
        userData = null;
        _composer.SetAsCurrentComposer();
        Clear();
        __ContentImpl(content, _composer, 0b_10);
        _composer.ResetAsCurrentComposer();
    }

    [Composable]
    private void ContentImpl(ComposableContent<Composer, int> content)
    {
        var composer = CurrentComposer;
        composer.StartReusableGroup(0);
        composer.SetVisualElement(this);
        composer.EnterVisualElement(this);
        CompositionLocalProvider(
            LocalVisualElement.Provides(this),
            () => content(_composer, 0)
        );
        composer.EndReusableGroup(0);
    }

    public override string ToString() => "ComposeView";
}