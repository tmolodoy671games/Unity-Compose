using System.Diagnostics.CodeAnalysis;
using StableCollections;
using UnityCompose;
using UnityEngine;
using UnityEngine.UIElements;

[SuppressMessage("ReSharper", "CheckNamespace")]
public partial class ComposeView : VisualElement
{
#pragma warning disable CS0618 // Type or member is obsolete
    public new class UxmlFactory : UxmlFactory<ComposeView, UxmlTraits>
#pragma warning restore CS0618 // Type or member is obsolete
    {
    }

    public readonly Composer Composer = new();
    private ComposableContent<Composer, int>? _content;
    private SlotTableType _slotTableType;

    public SlotTableType Type
    {
        get => _slotTableType;
        set
        {
            if (_slotTableType == value)
                return;
            _slotTableType = value;
            Clear();
            Composer.Clear();
            Composer.SetSlotTableType(value);
        }
    }

    public void SetContent(ComposableContent<Composer, int> content)
    {
        pickingMode = PickingMode.Ignore;
        if (_content == content)
            return;
        _content = content;
        userData = null;
        Composer.SetAsCurrentComposer();
        Clear();
        Composer.Clear();
        __ContentImpl(content, Composer, 0b_10);
        Composer.ResetAsCurrentComposer();
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
            LocalModalMenuVisibility.Provides(onScreenManager.Contents.IsNotEmpty()),
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

public enum SlotTableType
{
    Stable,
    Performant,
}