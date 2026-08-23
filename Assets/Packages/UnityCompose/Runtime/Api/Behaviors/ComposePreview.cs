using System.Diagnostics.CodeAnalysis;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

[DisallowMultipleComponent, ExecuteAlways]
public abstract partial class ComposePreview : MonoBehaviour
{
    [SerializeField] private bool pin;

    protected virtual SlotTableType SlotTableType => SlotTableType.Stable;

    [Composable]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    protected abstract void Preview();

    private void Awake()
    {
        if (!ApplicationUtils.IsPlaying) return;
        gameObject.SetActive(false);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (ApplicationUtils.IsPlaying) return;
        var isSelected = pin || Selection.activeGameObject == gameObject;
        var document = GetComponent<UIDocument>();
        if (!document) return;
        var composeView = document.rootVisualElement?.Q<ComposeView>();
        if (composeView != null)
            composeView.Type = SlotTableType;
        composeView?.SetContent(isSelected ? __Preview : __EmptyPreview);
#endif
    }

    [Composable]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private static void EmptyPreview()
    {
    }
}