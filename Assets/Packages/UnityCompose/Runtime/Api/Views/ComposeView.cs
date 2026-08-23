using System.Diagnostics.CodeAnalysis;
using StableCollections;
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
        _composer.Clear();
        __ContentImpl(content, _composer, 0b_10);
        _composer.ResetAsCurrentComposer();
    }

    [Composable]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private void ContentImpl(ComposableContent<Composer, int> content)
    {
        var onScreenManager = Remember(() => new ModalMenuManager());
        var composer = CurrentComposer;
        composer.StartReusableGroup<ComposeView>(0);
        composer.SetVisualElement(this);
        composer.EnterVisualElement(this);
        var isActiveInstance = Remember(onScreenManager.Contents.IsEmpty(),
            () => new IsActiveEntry(onScreenManager.Contents.IsEmpty(), null)
        );
        CompositionLocalProvider(
            LocalVisualElement.Provides(this),
            LocalIsActive.Provides(isActiveInstance),
            LocalOnScreenMenuManager.Provides(onScreenManager),
            () => content(composer, 0)
        );
        foreach (var overlayContent in onScreenManager.Contents)
        {
            Box(
                modifier: Modifier
                    .OnClick(() => { })
                    .FillMaxSize()
                    .Float(),
                content: overlayContent
            );
        }

        composer.EndReusableGroup(0);
    }

    public override string ToString() => "ComposeView";
}