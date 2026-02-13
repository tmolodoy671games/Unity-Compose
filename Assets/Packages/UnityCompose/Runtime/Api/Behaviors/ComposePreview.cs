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
    
    [Composable]
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
        composeView?.SetContent(isSelected ? __Preview : __EmptyPreview);
#endif
    }

    [Composable]
    private static void EmptyPreview()
    {
    }
}