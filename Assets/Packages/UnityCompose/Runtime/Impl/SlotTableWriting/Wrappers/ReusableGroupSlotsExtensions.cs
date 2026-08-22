using System;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Wrappers;

internal static class ReusableGroup
{
    public const int MetadataSize = 1;
    public const int ReusableNodeOffset = 0;
}

internal static class ReusableGroupSlotsExtensions
{
    public static ReusableComposeNode<T> GetReusableNode<T>(this Slots slots, int index) where T : VisualElement, new()
    {
        var existingNode = slots[index + ReusableGroup.ReusableNodeOffset];
        if (existingNode is not ReusableComposeNode<T> reusableNode)
        {
            if (existingNode is IComposeDisposable disposable)
                disposable.Dispose();
            reusableNode = ReusableComposeNode.Get<T>();
            slots[index + ReusableGroup.ReusableNodeOffset] = reusableNode;
        }
        return reusableNode;
    }
    
    public static ReusableComposeNode? GetReusableNode(this Slots slots, int index)
    {
        var existingNode = slots[index + ReusableGroup.ReusableNodeOffset];
        return existingNode as ReusableComposeNode;
    }

    public static void SetVisualElement<T>(this Slots slots, int index, T visualElement) where T : VisualElement, new()
    {
        var existingNode = slots[index + ReusableGroup.ReusableNodeOffset];
        if (existingNode is not ReusableComposeNode<T> reusableNode)
        {
            if (existingNode is IComposeDisposable disposable)
                disposable.Dispose();
            reusableNode = ReusableComposeNode.Get<T>();
            slots[index + ReusableGroup.ReusableNodeOffset] = reusableNode;
        }
        reusableNode.VisualElement = visualElement;
    }

    public static void InsertVisualElement(this Slots slots, int index)
    {
        slots.Insert(index + ReusableGroup.ReusableNodeOffset, ComposeEmptySlot.Instance);
    }
}