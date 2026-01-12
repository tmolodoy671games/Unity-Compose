using System;
using System.Collections.Generic;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public abstract class ComposeCallback
{
    public abstract void Clear();
}

public class ComposeCallback<T> : ComposeCallback where T : EventBase
{
    private readonly List<Action<T>> _callbacks = new(1);

    public readonly EventCallback<T> Callback;
    private T? _lastEvent;

    public ComposeCallback()
    {
        Callback = it =>
        {
            InvokedAtFrame = Time.frameCount;
            _lastEvent = it;
            foreach (var callback in _callbacks)
                callback(it);
        };
    }

    public int InvokedAtFrame { get; private set; }

    public void Add(Action<T> callback)
    {
        if (_callbacks.Contains(callback))
            return;
        _callbacks.Add(callback);
    }

    public void Remove(Action<T> callback)
    {
        _callbacks.Remove(callback);
    }

    public override void Clear()
    {
        _callbacks.Clear();
    }

    public void ReInvoke()
    {
        if (_lastEvent == null)
            return;
        Callback(_lastEvent);
    }
}

public class ComposeCallback<TKey, T> : ComposeCallback where T : EventBase
{
    private readonly Dictionary<TKey, Action<T>> _callbacks = new(1);

    public readonly EventCallback<T> Callback;
    private T? _lastEvent;

    public ComposeCallback()
    {
        Callback = it =>
        {
            InvokedAtFrame = Time.frameCount;
            _lastEvent = it;
            foreach (var callback in _callbacks)
                callback.Value(it);
        };
    }

    public int InvokedAtFrame { get; private set; }

    public void Add(TKey key, Action<T> callback)
    {
        _callbacks[key] = callback;
    }

    public void Remove(TKey key)
    {
        _callbacks.Remove(key);
    }
    
    public override void Clear()
    {
        _callbacks.Clear();
    }

    public void ReInvoke()
    {
        if (_lastEvent == null)
            return;
        Callback(_lastEvent);
    }
}

public static partial class VisualElementExtensions
{
    public static ComposeCallback<T> GetComposeCallback<T>(this VisualElement element) where T : EventBase<T>, new()
    {
        var cachedDictionary = element.Callbacks();
        if (cachedDictionary.TryGet(typeof(T), out var callback))
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
        var cachedDictionary = element.CallbacksOrNull();
        if (cachedDictionary == null)
            return;
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
    
    public static void RemoveTransition(this VisualElement element,string name)
    {
        if (element.style.transitionProperty.value == null)
            return;

        var index = element.style.transitionProperty.value.IndexOf(name);
        if (index < 0)
            return;
        element.style.transitionProperty.value.RemoveAt(index);
        element.style.transitionDuration.value.RemoveAt(index);
        element.style.transitionDelay.value.RemoveAt(index);
        element.style.transitionTimingFunction.value.RemoveAt(index);
    }

    public static void AddTransitions(this VisualElement element, ComposeTransition transition, params string[] names)
    {
        foreach (var name in names)
            element.AddTransition(transition, name);
    }

    public static IMutableStableDictionary<object, object?> UserData(this VisualElement element)
    {
        if (element.userData is IMutableStableDictionary<object, object?> cachedUserData)
            return cachedUserData;
        var newUserData = IMutableStableDictionary.Create<object, object?>();
        element.userData = newUserData;
        return newUserData;
    }

    private static IMutableStableDictionary<Type, ComposeCallback> Callbacks(this VisualElement element)
    {
        var userData = element.UserData();
        if (userData.TryGet("__Callbacks", out var cached) &&
            cached is IMutableStableDictionary<Type, ComposeCallback> cachedCallbacks)
            return cachedCallbacks;
        var newCallbacks = IMutableStableDictionary.Create<Type, ComposeCallback>();
        userData["__Callbacks"] = newCallbacks;
        return newCallbacks;
    }

    private static IMutableStableDictionary<Type, ComposeCallback>? CallbacksOrNull(this VisualElement element)
    {
        var userData = element.UserData();
        if (userData.TryGet("__Callbacks", out var cached) &&
            cached is IMutableStableDictionary<Type, ComposeCallback> cachedCallbacks)
            return cachedCallbacks;
        return null;
    }
}