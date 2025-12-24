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
    private int _gapStart;
    private int _gapLength;
    private Action<ItemsShiftEvent>? _onItemsShift;

    public GapBufferList(int capacity = 10, int initialGapSize = 10)
    {
        _array = new T[capacity];
        _gapLength = initialGapSize;
        DesiredGapSize = initialGapSize;
    }

    public int Count { get; private set; }
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
        _gapLength++;
        Count--;
    }

    public void RemoveRange(int index, int count)
    {
        MoveGapAt(index);
        _gapLength += count;
        Count -= count;
    }

    public void Insert(int index, T item)
    {
        EnsureGapSize(1);
        MoveGapAt(index);
        EnsureCapacity(Count + _gapLength + 1);
        _array[index] = item;
        _gapStart++;
        _gapLength--;
        Count++;
    }

    public void InsertRange(int index, List<T> items)
    {
        EnsureGapSize(items.Count);
        MoveGapAt(index);
        EnsureCapacity(Count + _gapLength + items.Count);
        for (var i = 0; i < items.Count; i++)
            _array[i + index] = items[i];
        _gapStart += items.Count;
        _gapLength -= items.Count;
        Count += items.Count;
    }

    public void Add(T item)
    {
        Insert(Count, item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        int firstPartCount = _gapStart;
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
                _gapStart + _gapLength,
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
        _gapStart = 0;
        _gapLength = _array.Length;
        Count = 0;
    }

    public IEnumerable<Optional<T>> GetItems()
    {
        for (var i = 0; i < Math.Min(Count + _gapLength, _array.Length); i++)
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
        if (logicalIndex < _gapStart)
            return logicalIndex;

        return logicalIndex + _gapLength;
    }

    public int AbsoluteToLogicalIndex(int absoluteIndex)
    {
        if (absoluteIndex != Count + _gapLength && absoluteIndex >= _gapStart && absoluteIndex < _gapStart + _gapLength)
            throw new ArgumentOutOfRangeException(nameof(absoluteIndex));

        if (absoluteIndex < _gapStart)
            return absoluteIndex;

        return absoluteIndex - _gapLength;
    }

    public override string ToString()
    {
        var items = GetItems();
        return $"Count: {Count}, Gap Length: {_gapLength}, Array Length: {_array.Length}\n" + items
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

        // Debug.Log($"MoveGapAt({index})");
        if (index < _gapStart)
        {
            // Left side:
            var count = _gapStart - index;
            _onItemsShift?.Invoke(new ItemsShiftEvent(index, count, _gapLength));
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
            // Right side:
            var count = index - _gapStart;
            _onItemsShift?.Invoke(new ItemsShiftEvent(_gapStart + _gapLength, count, -_gapLength));
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
        if (_gapLength > insertionCount)
            return;
        EnsureCapacity(Count + DesiredGapSize);
        if (_gapStart != Count)
        {
            var count = Count - (_gapStart + _gapLength) + 1;
            _onItemsShift?.Invoke(new ItemsShiftEvent(_gapStart + _gapLength, count, DesiredGapSize - _gapLength));
            Array.Copy(
                sourceArray: _array,
                destinationArray: _array,
                sourceIndex: _gapStart + _gapLength,
                destinationIndex: _gapStart + DesiredGapSize,
                length: count
            );
        }

        _gapLength = DesiredGapSize;
    }
}

internal readonly record struct ItemsShiftEvent(
    int StartIndex,
    int Count,
    int Offset
);