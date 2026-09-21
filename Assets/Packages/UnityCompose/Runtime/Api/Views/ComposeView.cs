using System.Diagnostics.CodeAnalysis;
using StableCollections;
using UnityCompose;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;

[SuppressMessage("ReSharper", "CheckNamespace")]
public partial class ComposeView : VisualElement
{
#pragma warning disable CS0618 // Type or member is obsolete
    public new class UxmlFactory : UxmlFactory<ComposeView, UxmlTraits>
    {
    }
#pragma warning restore CS0618 // Type or member is obsolete

    private readonly ComposerImpl _composer = new();
    private ComposableContent? _content;
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
            _composer.Clear();
            _composer.SetSlotTableType(value);
        }
    }

    [SuppressMessage("Compose.Net", "CN001:Invalid Composable Member Call Site")]
    public void SetContent(ComposableContent content)
    {
        pickingMode = PickingMode.Ignore;
        if (Equals(_content, content))
            return;
        _content = content;
        userData = null;
        _composer.SetAsCurrentComposer();
        Clear();
        _composer.Clear();
        ContentImpl(content);
        _composer.ResetAsCurrentComposer();
    }

    [Composable]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private void ContentImpl(ComposableContent content)
    {
        var onScreenManager = Remember(() => new ModalMenuManager());
        var composer = (ComposerImpl)CurrentComposer;
        composer.StartReusableGroup<ComposeView>(0);
        composer.SetVisualElement(this);
        composer.EnterVisualElement(this);
        var focusManager = Remember(this.FocusManager);
        // BRUH
        // CompositionLocalProvider(
        //     LocalVisualElement.Provides(this),
        //     LocalOnScreenMenuManager.Provides(onScreenManager),
        //     LocalModalMenuTags.Provides(onScreenManager.Tags),
        //     LocalFocusManager.Provides(focusManager),
        //     () => WithIsActive(
        //         onScreenManager.Contents.IsEmpty(),
        //         content
        //     )
        // );
        // foreach (var overlayContent in onScreenManager.Contents)
        // {
        //     Box(
        //         modifier: Modifier
        //             .OnClick(() => { })
        //             .FillMaxSize()
        //             .Float(),
        //         content: overlayContent
        //     );
        // }

        composer.EndReusableGroup(0);
    }

    public override string ToString() => "ComposeView";
}

public enum SlotTableType
{
    Stable,
    Performant,
}