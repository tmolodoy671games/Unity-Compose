using System;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SturdySlotTable.Models;

internal class SturdyElementIndexManager : IDisposable
{
    private static readonly ObjectPool<SturdyElementIndexManager> Pool = new(
        () => new SturdyElementIndexManager(),
        onInit: it => it._isDisposed = false
    );

    public static SturdyElementIndexManager Get() => Pool.Get();

    private readonly IMutableStableList<SturdyAnchor> _anchors =
        MutableStableListOf<SturdyAnchor>();

    private readonly IMutableStableList<SturdyAnchor> _anchorsToRemove =
        MutableStableListOf<SturdyAnchor>();

    private bool _isDisposed;

    private SturdyElementIndexManager()
    {
    }

    public SturdyAnchor GetAnchor(int index, int composition)
    {
        var lastAnchor = _anchors!.GetOrDefault(_anchors.LastIndex, null);
        if (lastAnchor == null || lastAnchor.Index != index || lastAnchor.Composition != composition)
        {
            lastAnchor = SturdyAnchor.Get(index, composition);
            _anchors.Add(lastAnchor);
        }

        return lastAnchor;
    }

    public void NotifyInsert(int index, int count, int composition)
    {
        foreach (var anchor in _anchors)
        {
            if (anchor.Composition < composition && anchor.Index >= index)
                anchor.Index += count;
        }
    }

    public void NotifyRemove(int index, int count, int composition)
    {
        foreach (var anchor in _anchors)
        {
            if (anchor.Composition == composition)
                continue;
            if (anchor.Index >= index + count)
                anchor.Index -= count;
            else if (anchor.Index >= index && anchor.Index < index + count)
                _anchorsToRemove.Add(anchor);
        }

        if (_anchorsToRemove.IsEmpty())
            return;
        foreach (var anchor in _anchorsToRemove)
            anchor.Dispose();
        _anchors.RemoveRange(_anchorsToRemove);
        _anchorsToRemove.Clear();
    }

    public void NotifySwap(
        int firstIndex,
        int firstCount,
        int secondIndex,
        int secondCount,
        int composition
    )
    {
        if (firstCount <= 0 || secondCount <= 0)
            return;

        if (firstIndex == secondIndex)
            return;

        // Normalize so that first range is before second range.
        if (firstIndex > secondIndex)
        {
            (firstIndex, secondIndex) = (secondIndex, firstIndex);
            (firstCount, secondCount) = (secondCount, firstCount);
        }

        var firstEnd = firstIndex + firstCount;
        var secondEnd = secondIndex + secondCount;

        // Ranges must not overlap.
        if (firstEnd > secondIndex)
            throw new ArgumentException("Swap ranges must not overlap.");

        var sizeDelta = secondCount - firstCount;

        foreach (var anchor in _anchors)
        {
            if (anchor.Composition == composition)
                continue;
            var value = anchor.Index;

            if (value >= firstIndex && value < firstEnd)
            {
                // First range moves to second range's old position.
                anchor.Index = secondIndex + (value - firstIndex);
            }
            else if (value >= secondIndex && value < secondEnd)
            {
                // Second range moves to first range's old position.
                anchor.Index = firstIndex + (value - secondIndex);
            }
            else if (value >= firstEnd && value < secondIndex)
            {
                // Elements between the ranges shift because the ranges exchanged
                // their positions.
                anchor.Index = value + sizeDelta;
            }
        }
    }

    public void Dispose()
    {
        if (_isDisposed)
            return;
        _isDisposed = true;
        _anchors.Clear();
        Pool.Return(this);
    }
}

internal class SturdyAnchor : IDisposable
{
    private static readonly ObjectPool<SturdyAnchor> Pool = new ObjectPool<SturdyAnchor>(
        factory: () => new SturdyAnchor(),
        onInit: it => it._isDisposed = false
    );

    public static SturdyAnchor Get(int index, int composition)
    {
        var result = Pool.Get();
        result.Index = index;
        result.Composition = composition;
        return result;
    }

    private bool _isDisposed;

    private SturdyAnchor()
    {
    }

    public int Index { get; set; }
    public int Composition { get; set; }

    public void Dispose()
    {
        if (_isDisposed)
            return;
        _isDisposed = true;
        Index = 0;
        Composition = 0;
        Pool.Return(this);
    }
}

internal static class VisualElementExtensions
{
    private const string Key = "UnityCompose_ElementManager";

    public static SturdyElementIndexManager? ElementManagerOrNull(this VisualElement element)
    {
        var map = element.UserData();
        return map.GetOrNull(Key) as SturdyElementIndexManager;
    }

    public static SturdyElementIndexManager RequireElementManager(this VisualElement element)
    {
        var elementManager = element.ElementManagerOrNull();
        if (elementManager == null)
        {
            elementManager = SturdyElementIndexManager.Get();
            element.UserData()[Key] = elementManager;
        }

        return elementManager;
    }
}