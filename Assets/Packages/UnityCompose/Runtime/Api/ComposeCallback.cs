using System;
using System.Collections.Generic;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class VisualElementExtensions
{
    public static VisualElement VisualElement(this EventBase evt)
    {
        return (VisualElement)evt.target;
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
    
    public static void RemoveTransitions(this VisualElement element, params string[] names)
    {
        foreach (var name in names)
            element.RemoveTransition(name);
    }


    public static IMutableStableDictionary<object, object?> UserData(this VisualElement element)
    {
        if (element.userData is IMutableStableDictionary<object, object?> cachedUserData)
            return cachedUserData;
        var newUserData = IMutableStableDictionary.Create<object, object?>();
        element.userData = newUserData;
        return newUserData;
    }
}