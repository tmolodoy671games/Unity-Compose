using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

[DisallowMultipleComponent, ExecuteAlways]
public abstract partial class ComposeUI : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<UIDocument>().rootVisualElement.Q<ComposeView>().SetContent(ContentImpl);
    }

    [Composable]
    protected abstract void Content();

    [Composable]
    private void ContentImpl()
    {
        if (!ApplicationUtils.IsPlaying)
            return;
        Content();
    }

    [Composable]
    protected virtual void Preview()
    {
    }

    private static void Spacer()
    {
    }
}

public abstract partial class ComposeUI
{
#if UNITY_EDITOR

    private void Update()
    {
        if (ApplicationUtils.IsPlaying) return;
        var isSelected = Selection.activeGameObject == gameObject;
        var document = GetComponent<UIDocument>();
        if (!document) return;
        var composeView = document.rootVisualElement?.Q<ComposeView>();
        composeView?.SetContent(isSelected ? Preview : EmptyPreview);
    }

    private static void EmptyPreview()
    {
    }
#endif
}