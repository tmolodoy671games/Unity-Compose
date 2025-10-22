using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

[DisallowMultipleComponent, ExecuteAlways]
public abstract class ComposePreview : MonoBehaviour
{
    [SerializeField] private bool pin;
    
    [Composable]
    protected abstract void Preview();

    private void Awake()
    {
        if (!ApplicationUtils.IsPlaying) return;
        gameObject.SetActive(false);
    }

#if UNITY_EDITOR

    private void Update()
    {
        if (ApplicationUtils.IsPlaying) return;
        var isSelected = pin || Selection.activeGameObject == gameObject;
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