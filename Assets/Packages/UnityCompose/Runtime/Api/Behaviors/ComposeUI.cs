using Sirenix.OdinInspector;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

[DisallowMultipleComponent, ExecuteAlways, HideMonoScript]
public abstract partial class ComposeUI : MonoBehaviour
{
    private void Awake()
    {
        if (!ApplicationUtils.IsPlaying)
            return;
        GetComponent<UIDocument>().rootVisualElement.Q<ComposeView>().SetContent(__Content);
    }

    [Composable]
    protected abstract void Content();

    [Composable]
    protected virtual void Preview()
    {
    }

    private void OnEnable()
    {
        if (ApplicationUtils.IsPlaying)
            return;
        GetComponent<UIDocument>()?.rootVisualElement?.Q<ComposeView>()?.SetContent(__Preview);
    }

    [Button]
    protected void PrintTreeStructure()
    {
        Debug.Log(CurrentComposer);
    }

    [Button]
    protected void PrintSlots()
    {
        Debug.Log(CurrentComposer.SlotsToString());
    }
}