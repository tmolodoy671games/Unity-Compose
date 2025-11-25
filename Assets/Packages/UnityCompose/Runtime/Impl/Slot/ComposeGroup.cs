using System.Runtime.CompilerServices;
using System.Text;
using SharpExtensions;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;

internal readonly record struct ComposeGroup(
    int ParentIndex,
    int Key,
    int Size,
    IComposeGroupState State
)
{
    public override string ToString()
    {
        var builder = new StringBuilder("(");
        builder.Append($"Key: {Key}, ");
        builder.Append($"Size: {Size}, ");
        builder.Append(")");
        return builder.ToString();
    }
}

internal static class GroupExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VisualElement? ElementOrNull(this ComposeGroup group)
    {
        return group.State.CastToOrNull<IComposeGroupState.Reusable>()?.Element;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IComposeGroupState.Replaceable? RememberedValueOrNull(this ComposeGroup group)
    {
        return group.State.CastToOrNull<IComposeGroupState.Replaceable>();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSameReusableGroup<T>(this ComposeGroup group, int key)
    {
        return group.Key == key && group.State is IComposeGroupState.Reusable<T>;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSameReplaceableGroup<TKey, TValue>(this ComposeGroup group, int key)
    {
        return group.Key == key && group.State is IComposeGroupState.Replaceable<TKey, TValue>;
    }
}