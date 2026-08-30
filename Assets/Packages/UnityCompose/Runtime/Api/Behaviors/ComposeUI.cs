using System.Diagnostics.CodeAnalysis;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

[DisallowMultipleComponent, ExecuteAlways]
public abstract partial class ComposeUI : MonoBehaviour
{
    private UIDocument? _document;

    protected virtual SlotTableType SlotTableType => SlotTableType.Stable;

    private void Awake()
    {
        if (!ApplicationUtils.IsPlaying)
            return;
        _document ??= GetUiDocument();
        var composeView = _document.rootVisualElement.Q<ComposeView>();
        composeView.Type = SlotTableType;
        _document.rootVisualElement.Q<ComposeView>().SetContent(__Content);
    }

    [Composable]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    protected abstract void Content();

    [Composable]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    protected virtual void Preview()
    {
    }

    private void OnEnable()
    {
        _document ??= GetUiDocument();
        var composeView = _document?.rootVisualElement?.Q<ComposeView>();
        if (ApplicationUtils.IsPlaying)
        {
            if (composeView != null)
                composeView.Type = SlotTableType;
            composeView?.SetContent(__Content);
            return;
        }

        composeView?.SetContent(__Preview);
    }

    private UIDocument GetUiDocument() => GetComponent<UIDocument>().NotNull();
}