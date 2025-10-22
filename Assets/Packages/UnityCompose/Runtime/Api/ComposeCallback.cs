using System;
using System.Collections.Generic;
using StableCollections;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public abstract class ComposeCallback
{
    public abstract void Clear();
}

public class ComposeCallback<T> : ComposeCallback where T : EventBase
{
    private readonly IMutableStableList<Action<T>> _callbacks = IMutableStableList.Create<Action<T>>();

    public readonly EventCallback<T> Callback;

    public ComposeCallback()
    {
        Callback = it =>
        {
            foreach (var callback in _callbacks)
                callback(it);
        };
    }

    public void Add(Action<T> callback)
    {
        _callbacks.Add(callback);
    }

    public void Add(Action callback)
    {
        _callbacks.Add(_ => callback());
    }

    public override void Clear()
    {
        _callbacks.Clear();
    }
}

public static class VisualElementExtensions
{
    public static ComposeCallback<T> GetComposeCallback<T>(this VisualElement element) where T : EventBase<T>, new()
    {
        var cachedDictionary = element.userData as IMutableStableDictionary<Type, ComposeCallback>;
        if (cachedDictionary == null)
            cachedDictionary = IMutableStableDictionary.Create<Type, ComposeCallback>();
        element.userData = cachedDictionary;
        if (cachedDictionary.TryGet(typeof(T), out ComposeCallback callback))
            return (ComposeCallback<T>)callback;
        var newCallback = new ComposeCallback<T>();
        cachedDictionary[typeof(T)] = newCallback;
        element.RegisterCallback(newCallback.Callback);
        return newCallback;
    }

    public static VisualElement VisualElement(this EventBase evt)
    {
        return (VisualElement)evt.target;
    }

    public static void ClearCallbacks(this VisualElement element)
    {
        var cachedDictionary = element.userData as IMutableStableDictionary<Type, ComposeCallback>;
        if (cachedDictionary == null) return;
        foreach (var callback in cachedDictionary.Values)
            callback.Clear();
    }

    public static void AddTransition(this VisualElement element, ComposeTransition transition, string name)
    {
        element.style.transitionProperty =
            element.style.transitionProperty.value ?? new List<StylePropertyName>();
        element.style.transitionDuration = element.style.transitionDuration.value ?? new List<TimeValue>();
        element.style.transitionDelay = element.style.transitionDelay.value ?? new List<TimeValue>();
        element.style.transitionTimingFunction =
            element.style.transitionTimingFunction.value ?? new List<EasingFunction>();

        element.style.transitionProperty.value.Add(name);
        element.style.transitionDuration.value.Add(transition.Duration);
        element.style.transitionDelay.value.Add(transition.Delay);
        element.style.transitionTimingFunction.value.Add(transition.TimingFunction);
    }

    public static void AddTransitions(this VisualElement element, ComposeTransition transition, params string[] names)
    {
        foreach (var name in names)
            element.AddTransition(transition, name);
    }
}