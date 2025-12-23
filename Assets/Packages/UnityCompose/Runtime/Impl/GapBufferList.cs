using System;
using System.Collections.Generic;
using System.Linq;
using SharpExtensions;
using UnityEngine;

// ReSharper disable CheckNamespace

namespace UnityCompose;

internal class GapBufferList<T>
{
    private const int DesiredGapSize = 10;

    private T[] _array = new T[10];
    private int _gapStart;
    private int _gapLength = DesiredGapSize;
    private Action<ItemsShiftEvent>? _onItemsShift;

    public int Count { get; private set; }

    public T this[int index]
    {
        get
        {
            if (IsIndexInsideGap(index))
                throw new ArgumentOutOfRangeException("Cannot access items at gap!");
            if (index >= Count)
                throw new ArgumentOutOfRangeException();
            return _array[index];
        }
        set => _array[index] = value;
    }

    public void AddItemsShiftObserver(Action<ItemsShiftEvent> onItemsShift)
    {
        _onItemsShift += onItemsShift;
    }

    public void RemoveItemsShiftObserver(Action<ItemsShiftEvent> onItemsShift)
    {
        _onItemsShift -= onItemsShift;
    }

    public void RemoveAt(int index)
    {
        MoveGapAt(index);
        _gapStart--;
        Count--;
    }

    public void InsertAt(int index, T item)
    {
        MoveGapAt(index);
        EnsureCapacity(1);
        EnsureGapSize(1);
        _array[index] = item;
        _gapStart++;
        _gapLength--;
        Count++;
    }

    public void Add(T item)
    {
        InsertAt(Count, item);
        // EnsureCapacity(1);
        // _array[Count] = item;
        // Count++;
    }

    public IEnumerable<Optional<T>> GetItems()
    {
        for (var i = 0; i < Count + _gapLength; i++)
        {
            if (IsIndexInsideGap(i))
                yield return Optional.Empty<T>();
            else
                yield return _array[i];
        }
    }
    
    public int LogicalToAbsoluteIndex(int logicalIndex)
    {
        if (logicalIndex < _gapStart)
            return logicalIndex;

        return logicalIndex + _gapLength;
    }
    
    public int AbsoluteToLogicalIndex(int absoluteIndex)
    {
        if (absoluteIndex >= _gapStart && absoluteIndex < _gapStart + _gapLength)
            throw new ArgumentOutOfRangeException(nameof(absoluteIndex));

        if (absoluteIndex < _gapStart)
            return absoluteIndex;

        return absoluteIndex - _gapLength;
    }

    public override string ToString()
    {
        var items = GetItems();
        return $"Count: {Count}, Array Length: {_array.Length}, Gap Length: {_gapLength}\n" + items
            .Select((it, index) => $"[{index}]\t" + (it.HasValue ? it.ToString() : "_"))
            .JoinToString("\n");
    }

    private bool IsIndexInsideGap(int index)
    {
        return index >= _gapStart && index < _gapStart + _gapLength;
    }

    private void MoveGapAt(int index)
    {
        if (_gapStart == index)
            return;

        if (index < _gapStart)
        {
            // Left
            var count = _gapStart - index;
            _onItemsShift?.Invoke(new ItemsShiftEvent(index, _gapLength, count));
            Array.Copy(
                sourceArray: _array,
                sourceIndex: index,
                destinationArray: _array,
                destinationIndex: index + _gapLength,
                length: count
            );
        }
        else
        {
            // Right
            var count = index - _gapStart;
            _onItemsShift?.Invoke(new ItemsShiftEvent(index, _gapLength, -count));
            Array.Copy(
                sourceArray: _array,
                sourceIndex: _gapStart + _gapLength,
                destinationArray: _array,
                destinationIndex: _gapStart,
                length: count
            );
        }

        _gapStart = index;
    }

    private void EnsureCapacity(int insertionCount)
    {
        if (_array.Length >= Count + insertionCount)
            return;
        var desiredSize = Math.Max(
            _array.Length * 2,
            (Count + insertionCount) * 2
        );
        Array.Resize(
            ref _array,
            desiredSize
        );
    }

    private void EnsureGapSize(int insertionCount)
    {
        if (_gapLength > insertionCount)
            return;
        EnsureCapacity(DesiredGapSize - _gapLength);
        _onItemsShift?.Invoke(
            new ItemsShiftEvent(
                _gapStart + _gapLength,
                Count - (_gapStart + _gapLength),
                DesiredGapSize - _gapLength
            )
        );
        _gapLength = DesiredGapSize;
    }
}

internal readonly record struct ItemsShiftEvent(
    int StartIndex,
    int Count,
    int Offset
);