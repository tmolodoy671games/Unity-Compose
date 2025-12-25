using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SharpExtensions;
using UnityEngine;

// ReSharper disable CheckNamespace

namespace UnityCompose;

internal class GapBufferList<T> : IList<T>
{
    private readonly int DesiredGapSize;

    private T[] _array;
    private Action<ItemsShiftEvent>? _onItemsShift;

    public GapBufferList(int capacity = 10, int initialGapSize = 10)
    {
        _array = new T[capacity];
        GapLength = initialGapSize;
        DesiredGapSize = initialGapSize;
    }

    public int Count { get; private set; }
    public int GapStart { get; private set; }
    public int GapLength { get; private set; }

    public bool IsReadOnly => false;

    public T this[int index]
    {
        get
        {
            index = LogicalToAbsoluteIndex(index);
            if (IsIndexInsideGap(index))
                throw new ArgumentOutOfRangeException();
            return _array[index];
        }
        set
        {
            index = LogicalToAbsoluteIndex(index);
            if (IsIndexInsideGap(index))
                throw new ArgumentOutOfRangeException();
            _array[index] = value;
        }
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= Count)
            throw new IndexOutOfRangeException(nameof(index));
        MoveGapAt(index);
        GapLength++;
        Count--;
    }

    public void RemoveRange(int index, int count)
    {
        MoveGapAt(index);
        GapLength += count;
        Count -= count;
    }

    public void Insert(int index, T item)
    {
        EnsureGapSize(1);
        MoveGapAt(index);
        EnsureCapacity(Count + GapLength + 1);
        _array[index] = item;
        GapStart++;
        GapLength--;
        Count++;
    }

    public void InsertRange(int index, List<T> items)
    {
        EnsureGapSize(items.Count);
        MoveGapAt(index);
        EnsureCapacity(Count + GapLength + items.Count);
        for (var i = 0; i < items.Count; i++)
            _array[i + index] = items[i];
        GapStart += items.Count;
        GapLength -= items.Count;
        Count += items.Count;
    }

    public void Add(T item)
    {
        Insert(Count, item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        int firstPartCount = GapStart;
        if (firstPartCount > 0)
        {
            Array.Copy(
                _array,
                0,
                array,
                arrayIndex,
                firstPartCount
            );
        }

        int secondPartCount = Count - firstPartCount;
        if (secondPartCount > 0)
        {
            Array.Copy(
                _array,
                GapStart + GapLength,
                array,
                arrayIndex + firstPartCount,
                secondPartCount
            );
        }
    }

    public bool Remove(T item)
    {
        var index = IndexOf(item);
        if (index < 0)
            return false;
        RemoveAt(index);
        return true;
    }

    public int IndexOf(T item)
    {
        for (var i = 0; i < Count; i++)
        {
            if (EqualityUtils.FastEquals(this[i], item))
                return i;
        }

        return -1;
    }

    public bool Contains(T item)
    {
        return IndexOf(item) >= 0;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var i = 0; i < Count; i++)
        {
            yield return this[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Clear()
    {
        GapStart = 0;
        GapLength = _array.Length;
        Count = 0;
    }

    public IEnumerable<Optional<T>> GetItems()
    {
        for (var i = 0; i < Math.Min(Count + GapLength, _array.Length); i++)
        {
            if (IsIndexInsideGap(i))
                yield return Optional.Empty<T>();
            else
                yield return _array[i];
        }
    }

    public void AddItemsShiftObserver(Action<ItemsShiftEvent> onItemsShift)
    {
        _onItemsShift += onItemsShift;
    }

    public void RemoveItemsShiftObserver(Action<ItemsShiftEvent> onItemsShift)
    {
        _onItemsShift -= onItemsShift;
    }

    public int LogicalToAbsoluteIndex(int logicalIndex)
    {
        if (logicalIndex < GapStart)
            return logicalIndex;

        return logicalIndex + GapLength;
    }

    public int AbsoluteToLogicalIndex(int absoluteIndex)
    {
        if (absoluteIndex != Count + GapLength && absoluteIndex >= GapStart && absoluteIndex < GapStart + GapLength)
            throw new ArgumentOutOfRangeException(nameof(absoluteIndex));

        if (absoluteIndex < GapStart)
            return absoluteIndex;

        return absoluteIndex - GapLength;
    }

    public override string ToString()
    {
        var items = GetItems();
        return $"Count: {Count}, Gap Length: {GapLength}, Array Length: {_array.Length}\n" + items
            .Select((it, index) => $"[{index}]\t" + (it.HasValue ? it.ToString() : "_"))
            .JoinToString("\n");
    }

    private bool IsIndexInsideGap(int index)
    {
        return index >= GapStart && index < GapStart + GapLength;
    }

    public void MoveGapAt(int index)
    {
        if (GapStart == index)
            return;

        // Debug.Log($"MoveGapAt({index})");
        if (index < GapStart)
        {
            // Left side:
            var count = GapStart - index;
            _onItemsShift?.Invoke(new ItemsShiftEvent(index, count, GapLength));
            Array.Copy(
                sourceArray: _array,
                sourceIndex: index,
                destinationArray: _array,
                destinationIndex: index + GapLength,
                length: count
            );
        }
        else
        {
            // Right side:
            var count = index - GapStart;
            _onItemsShift?.Invoke(new ItemsShiftEvent(GapStart + GapLength, count, -GapLength));
            Array.Copy(
                sourceArray: _array,
                sourceIndex: GapStart + GapLength,
                destinationArray: _array,
                destinationIndex: GapStart,
                length: count
            );
        }

        GapStart = index;
    }

    private void EnsureCapacity(int desiredSize)
    {
        if (_array.Length >= desiredSize)
            return;
        var newSize = Math.Max(
            _array.Length * 2,
            desiredSize * 2
        );
        Array.Resize(
            ref _array,
            newSize
        );
    }

    private void EnsureGapSize(int insertionCount)
    {
        if (GapLength > insertionCount)
            return;
        EnsureCapacity(Count + DesiredGapSize);
        if (GapStart != Count)
        {
            var count = Count - (GapStart + GapLength) + 1;
            _onItemsShift?.Invoke(new ItemsShiftEvent(GapStart + GapLength, count, DesiredGapSize - GapLength));
            Array.Copy(
                sourceArray: _array,
                destinationArray: _array,
                sourceIndex: GapStart + GapLength,
                destinationIndex: GapStart + DesiredGapSize,
                length: count
            );
        }

        GapLength = DesiredGapSize;
    }
}

internal readonly record struct ItemsShiftEvent(
    int StartIndex,
    int Count,
    int Offset
);