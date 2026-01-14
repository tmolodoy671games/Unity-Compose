using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SharpExtensions;
using StableCollections;
using UnityEngine;

// ReSharper disable CheckNamespace

namespace UnityCompose;

internal class GapBufferList<T> : IList<T>
{
    private const int LockOffset = 1_000_000_000;

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

    public void Move(int sourceIndex, int targetIndex, int count)
    {
        if (sourceIndex == targetIndex || count == 0)
            return;
        MoveGapAt(Count);
        var buffer = new T[count];

        // Copy to buffer:
        Array.Copy(
            sourceArray: _array,
            sourceIndex: sourceIndex,
            destinationArray: buffer,
            destinationIndex: 0,
            length: count
        );
        NotifyElementsShift(sourceIndex, count, LockOffset);

        // Remove space:
        Array.Copy(
            sourceArray: _array,
            destinationArray: _array,
            sourceIndex: sourceIndex + count,
            destinationIndex: sourceIndex,
            length: Count - (sourceIndex + count)
        );
        NotifyElementsShift(sourceIndex + count, Count - (sourceIndex + count), -count);

        // Allocate space:
        Array.Copy(
            sourceArray: _array,
            destinationArray: _array,
            sourceIndex: targetIndex,
            destinationIndex: targetIndex + count,
            length: Count - (targetIndex + count)
        );
        NotifyElementsShift(targetIndex, Count - (targetIndex + count), count);

        // Set elements:
        for (var i = 0; i < count; i++)
            _array[targetIndex + i] = buffer[i];
        NotifyElementsShift(sourceIndex + LockOffset, count, targetIndex - sourceIndex - LockOffset);
    }

    public void Swap(int sourceIndex, int sourceCount, int targetIndex, int targetCount)
    {
        if (sourceIndex == targetIndex || sourceCount == 0 && targetCount == 0)
            return;
        var initialCount = Count;
        MoveGapAt(Count);

        var (firstIndex, firstCount, secondIndex, secondCount) = sourceIndex < targetIndex
            ? (sourceIndex, sourceCount, targetIndex, targetCount)
            : (targetIndex, targetCount, sourceIndex, sourceCount);
        if (firstIndex + firstCount > secondIndex)
            throw new ArgumentException("Ranges are intersecting!");
        var firstBuffer = new T[firstCount];
        var secondBuffer = new T[secondCount];
        var areBuffersOfDifferentSizes = firstCount != secondCount;

        // Copy to buffers:
        Array.Copy(
            sourceArray: _array,
            destinationArray: firstBuffer,
            sourceIndex: firstIndex,
            destinationIndex: 0,
            length: firstCount
        );
        Array.Copy(
            sourceArray: _array,
            destinationArray: secondBuffer,
            sourceIndex: secondIndex,
            destinationIndex: 0,
            length: secondCount
        );

        // Remove space:
        if (areBuffersOfDifferentSizes)
        {
            Array.Copy(
                sourceArray: _array,
                destinationArray: _array,
                sourceIndex: secondIndex + secondCount,
                destinationIndex: secondIndex,
                length: Count - (secondIndex + secondCount)
            );
            Count -= secondCount;
            Array.Copy(
                sourceArray: _array,
                destinationArray: _array,
                sourceIndex: firstIndex + firstCount,
                destinationIndex: firstIndex,
                length: Count - (firstIndex + firstCount)
            );
            Count -= firstCount;
        }

        // Insert second buffer into first index:
        if (areBuffersOfDifferentSizes)
        {
            Array.Copy(
                sourceArray: _array,
                destinationArray: _array,
                sourceIndex: firstIndex,
                destinationIndex: firstIndex + secondCount,
                length: Count - firstIndex
            );
            Count += secondCount;
        }

        Array.Copy(
            sourceArray: secondBuffer,
            destinationArray: _array,
            sourceIndex: 0,
            destinationIndex: firstIndex,
            length: secondCount
        );

        // Insert first buffer into second index:
        var secondDestinationIndex = secondIndex + (secondCount - firstCount);
        if (areBuffersOfDifferentSizes)
        {
            Array.Copy(
                sourceArray: _array,
                destinationArray: _array,
                sourceIndex: secondDestinationIndex,
                destinationIndex: secondDestinationIndex + firstCount,
                length: Count - secondDestinationIndex
            );
            Count += firstCount;
        }

        Array.Copy(
            sourceArray: firstBuffer,
            destinationArray: _array,
            sourceIndex: 0,
            destinationIndex: secondDestinationIndex,
            length: firstCount
        );

        // Notify observers:
        // Lock ranges:
        Count = initialCount;
        NotifyElementsLock(firstIndex, firstCount);
        NotifyElementsLock(secondIndex, secondCount);

        // Move gap between:
        if (areBuffersOfDifferentSizes)
        {
            NotifyElementsShift(
                startIndex: firstIndex + firstCount,
                count: secondIndex - (firstIndex + firstCount),
                offset: secondCount - firstCount
            );
        }

        // Unlock ranges:
        NotifyElementsUnlock(firstIndex, firstCount, secondIndex - firstIndex);
        NotifyElementsUnlock(secondIndex, secondCount, firstIndex - secondIndex);
    }

    private void Log(string message)
    {
        Debug.Log($"{message}\n[{this.JoinToString()}]");
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
        // if (absoluteIndex != Count + GapLength && absoluteIndex >= GapStart && absoluteIndex < GapStart + GapLength)
        // {
        //     throw new ArgumentOutOfRangeException(
        //         $"Index {absoluteIndex} is inside the gap buffer: length: {_array.Length}, gapStart: {GapStart}, gapLength: {GapLength}"
        //     );
        // }

        if (absoluteIndex < GapStart)
            return absoluteIndex;

        return absoluteIndex - GapLength;
    }

    public override string ToString()
    {
        return $"[{this.JoinToString()}]";
        // var items = GetItems();
        // return $"Count: {Count}, Gap Length: {GapLength}, Array Length: {_array.Length}\n" + items
        //     .Select((it, index) => $"[{index}]\t" + (it.HasValue ? it.ToString() : "_"))
        //     .JoinToString("\n");
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
            NotifyElementsShift(index, count, GapLength);
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
            NotifyElementsShift(GapStart + GapLength, count, -GapLength);
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
            NotifyElementsShift(GapStart + GapLength, count, DesiredGapSize - GapLength);
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

    private void NotifyElementsShift(int startIndex, int count, int offset)
    {
        if (count == 0 || offset == 0)
            return;
        _onItemsShift?.Invoke(new ItemsShiftEvent(startIndex, count, offset));
    }

    private void NotifyElementsLock(int startIndex, int count)
    {
        NotifyElementsShift(startIndex, count, LockOffset);
    }

    private void NotifyElementsUnlock(int startIndex, int count, int offset)
    {
        NotifyElementsShift(startIndex + LockOffset, count, offset - LockOffset);
    }
}

internal readonly record struct ItemsShiftEvent(
    int StartIndex,
    int Count,
    int Offset
);