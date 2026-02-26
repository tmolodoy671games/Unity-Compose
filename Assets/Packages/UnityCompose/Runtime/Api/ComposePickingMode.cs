// ReSharper disable CheckNamespace

using UnityEngine.UIElements;

namespace UnityCompose;

public class ComposePickingMode
{
    private readonly VisualElement _element;
    private int _count;

    internal ComposePickingMode(VisualElement element)
    {
        _element = element;
    }

    public void Increment()
    {
        // if (_count == 0)
        //     _element.pickingMode = PickingMode.Position;
        _count++;
    }

    public void Decrement()
    {
        _count--;
        // if (_count == 0)
        //     _element.pickingMode = PickingMode.Ignore;
    }
}

public static partial class VisualElementExtensions
{
    public static ComposePickingMode ComposePickingMode(this VisualElement element)
    {
        const string key = "UnityCompose.ComposePickingMode";
        var userData = element.UserData();
        if (userData.TryGet(key, out var cachedInstance))
            return (ComposePickingMode)cachedInstance!;
        var newInstance = new ComposePickingMode(element);
        userData[key] = newInstance;
        return newInstance;
    }
}