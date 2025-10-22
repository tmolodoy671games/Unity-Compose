using System;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

internal static class ComposeGroupExtensions
{
    public static void RemoveSubGroup(this ComposeGroup parent, ComposeGroup child)
    {
        DisposeRecursively(child);
        parent.Children.Remove(child.Key);
        parent.RemoveSubGroupRecursively(child);
    }

    private static void RemoveSubGroupRecursively(this ComposeGroup parent, ComposeGroup child)
    {
        if (child.Element != null)
        {
            child.Element.parent?.Remove(child.Element);
            return;
        }

        foreach (var grandChild in child.Children.Values)
            parent.RemoveSubGroupRecursively(grandChild.ComposeGroup);
    }

    private static void DisposeRecursively(ComposeGroup composeGroup)
    {
        foreach (var rememberedValue in composeGroup.RememberedValues.Values)
        {
            if (rememberedValue.Value is IDisposable disposable)
                disposable.Dispose();
        }

        foreach (var child in composeGroup.Children.Values)
            DisposeRecursively(child.ComposeGroup);
    }

    public static ComposeGroup GetOrCreateSubGroup(this ComposeGroup parent, object key)
    {
        if (parent.Children.TryGet(key, out var cachedGroup))
        {
            cachedGroup.InvokedThisStep = true;
            return cachedGroup.ComposeGroup;
        }

        var group = new ComposeGroup(key, parent);
        var state = new ComposeGroupState(group)
        {
            InvokedThisStep = true
        };
        parent.Children[key] = state;
        return group;
    }

    public static object ResolveKey(this ComposeGroup parent, object key)
    {
        var invocationState = parent.GetOrCreateInvocationState(key);
        if (invocationState.InvocationCount == 0)
        {
            invocationState.InvocationCount++;
            return key;
        }

        invocationState.InvocationCount++;
        var newKey = (key, invocationState.InvocationCount - 1);
        invocationState = parent.GetOrCreateInvocationState(newKey);
        invocationState.InvocationCount++;
        return newKey;
    }

    private static ComposeInvocationState GetOrCreateInvocationState(this ComposeGroup parent, object key)
    {
        if (parent.Invocations.TryGet(key, out var cachedInvocationState))
            return cachedInvocationState;
        var invocationState = new ComposeInvocationState();
        parent.Invocations[key] = invocationState;
        return invocationState;
    }
}