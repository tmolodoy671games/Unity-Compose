using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Core;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Entities;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Utils;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;

internal abstract class ReusableComposeGroup : ComposeGroup
{
    public readonly List<ComposeGroup> Children = new(0);
    public readonly CompositionLocalMap CompositionLocalMap;
    private readonly SlotWriter _writer;

    public object? ObjectKey;
    public VisualElement? Element;
    public int ElementIndex;
    public int ElementsCount;
    public Action? Restart;

    protected ReusableComposeGroup(int key, ReusableComposeGroup? parent, SlotWriter writer) : base(key, parent)
    {
        CompositionLocalMap = new CompositionLocalMap(parent?.CompositionLocalMap);
        _writer = writer;
    }

    public void PerformRestart()
    {
        // Debug.Log($"Restart {this}");
        _writer.ResetTo(this);
        Restart?.Invoke();
        _writer.ResetToRoot();
    }
}

internal class ReusableComposeGroup<T> : ReusableComposeGroup
{
    public T PreviousState;

    public ReusableComposeGroup(int key, ReusableComposeGroup? parent, T previousState, SlotWriter writer) : base(key,
        parent, writer)
    {
        PreviousState = previousState;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append("(");
        builder.Append($"Key: {Key}");
        builder.Append($", IndexInParent: {IndexInParent}");
        builder.Append($", ElementIndex: {ElementIndex}");
        if (ElementsCount != 0)
        {
            builder.Append($", ElementsCount: {ElementsCount}");
        }

        if (Element != null)
            builder.Append($", Element: {Element.GetType().Name}");
        builder.Append(")");
        builder.Append($" {GetHashCode()}");
        return builder.ToString();
    }

    public override string ToString(ComposeGroup? currentParent, int currentIndex)
    {
        var ancestorsCount = this.Ancestors().Count();
        var builder = new StringBuilder();
        builder.Append("-".Multiply(ancestorsCount));
        builder.Append($"[{IndexInParent}] ");
        builder.Append(this);
        if (currentParent == this)
            builder.Append(" < CURRENT_PARENT");
        if (currentIndex == IndexInParent && currentParent == Parent)
            builder.Append(" < CURRENT_INDEX");
        if (Children.Count > 0)
            builder.AppendLine();
        for (var index = 0; index < Children.Count; index++)
        {
            var child = Children[index];
            builder.Append(child.ToString(currentParent, currentIndex));
            if (index != Children.Count - 1)
                builder.AppendLine();
        }

        return builder.ToString();
    }

    public override void Dispose()
    {
        var elementsCount = ElementsCount;
        if (elementsCount != 0)
        {
            foreach (var ancestor in this.Ancestors())
            {
                if (ancestor.Element != null)
                    break;
                ancestor.ElementsCount -= elementsCount;
            }
        }

        foreach (var child in Children)
            child.Dispose();
    }
}