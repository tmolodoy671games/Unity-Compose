using System;
using System.Collections.Generic;
using System.Linq;
using SharpExtensions;
using StableCollections;
using UnityCompose;
using UnityEngine;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl
{
    [DisallowMultipleComponent]
    internal class GapBufferTest : MonoBehaviour
    {
        private void Test()
        {
            var result = Optional.Empty<Color>().Equals(Optional.Empty<Color>());
            Debug.Log(result);
            // GapBufferListTests.RunAll();
        }
    }
}

public static class GapBufferListTests
{
    public static void RunAll()
    {
        Test_EmptyList();
        Test_Add();
        Test_IndexerGetSet();
        Test_Insert();
        Test_RemoveAt();
        Test_Remove();
        Test_Clear();
        Test_Contains();
        Test_CopyTo();
        Test_Enumeration();
        Test_InsertRemovePatterns();
        Test_BoundaryConditions();
        Test_RandomizedAgainstList();
        Test_Swap();
        Test_Swap_DifferentSizes();
        Test_RandomizedSwapAgainstList();
        Debug.Log("Tests Completed");
    }

    private static void Assert(bool condition, string message)
    {
        if (condition) return;
        Debug.LogError("[TEST FAILED] " + message);
    }

    private static void AssertSequence<T>(IList<T> list, IImmutableStableList<T> expected, string message)
    {
        Assert(list.SequenceEqual(expected),
            $"{message}: Expected {expected}, got {list.ToImmutableStableList()}");
    }

    private static void Test_EmptyList()
    {
        var list = new GapBufferList<int>();

        Assert(list.Count == 0, "Empty list Count != 0");
        Assert(!list.Contains(0), "Empty list Contains returned true");
    }

    private static void Test_Add()
    {
        var list = new GapBufferList<int> { 1, 2, 3 };

        AssertSequence(list, IImmutableStableList.Create(1, 2, 3), "Test_Add");
    }

    private static void Test_IndexerGetSet()
    {
        var list = new GapBufferList<int> { 10, 20, 30 };

        Assert(list[1] == 20, "Indexer get failed");

        list[1] = 99;
        AssertSequence(list, IImmutableStableList.Create(10, 99, 30), "Test_IndexerGetSet");
    }

    private static void Test_Insert()
    {
        var list = new GapBufferList<int> { 1, 3, 4 };

        list.Insert(1, 2);
        AssertSequence(list, IImmutableStableList.Create(1, 2, 3, 4), "Test_Insert");

        list.Insert(0, 0);
        AssertSequence(list, IImmutableStableList.Create(0, 1, 2, 3, 4), "Test_Insert");

        list.Insert(list.Count, 5);
        AssertSequence(list, IImmutableStableList.Create(0, 1, 2, 3, 4, 5), "Test_Insert");
    }

    private static void Test_RemoveAt()
    {
        var list = new GapBufferList<int> { 0, 1, 2, 3, 4 };

        list.RemoveAt(2);
        AssertSequence(list, IImmutableStableList.Create(0, 1, 3, 4), "Test_RemoveAt");

        list.RemoveAt(0);
        AssertSequence(list, IImmutableStableList.Create(1, 3, 4), "Test_RemoveAt");

        list.RemoveAt(list.Count - 1);
        AssertSequence(list, IImmutableStableList.Create(1, 3), "Test_RemoveAt");
    }

    private static void Test_Remove()
    {
        var list = new GapBufferList<int> { 1, 2, 3, 2 };

        bool removed = list.Remove(2);
        Assert(removed, "Remove returned false for existing value");
        AssertSequence(list, IImmutableStableList.Create(1, 3, 2), "Test_Remove");

        removed = list.Remove(999);
        Assert(!removed, "Remove returned true for non-existing value");
    }

    private static void Test_Clear()
    {
        var list = new GapBufferList<int> { 1, 2, 3 };

        list.Clear();
        Assert(list.Count == 0, "Clear did not reset Count");

        list.Add(42);
        AssertSequence(list, IImmutableStableList.Create(42), "Test_Clear");
    }

    private static void Test_Contains()
    {
        var list = new GapBufferList<int> { 5, 6, 7 };

        Assert(list.Contains(6), "Contains failed for existing value");
        Assert(!list.Contains(10), "Contains returned true for missing value");
    }

    private static void Test_CopyTo()
    {
        var list = new GapBufferList<int> { 1, 2, 3 };
        var array = new int[5];

        list.CopyTo(array, 1);

        Assert(array[1] == 1, "CopyTo incorrect at index 1");
        Assert(array[2] == 2, "CopyTo incorrect at index 2");
        Assert(array[3] == 3, "CopyTo incorrect at index 3");
    }

    private static void Test_Enumeration()
    {
        var list = new GapBufferList<int> { 1, 2, 3, 4 };

        int sum = 0;
        foreach (var v in list)
            sum += v;

        Assert(sum == 10, "Enumeration produced incorrect values");
    }

    private static void Test_InsertRemovePatterns()
    {
        var list = new GapBufferList<int>();

        for (int i = 0; i < 10; i++)
            list.Add(i);

        list.Insert(5, 100);
        list.Insert(5, 101);
        list.RemoveAt(6);

        AssertSequence(list, IImmutableStableList.Create(0, 1, 2, 3, 4, 101, 5, 6, 7, 8, 9), "Test_InsertRemovePatterns");
    }

    private static void Test_BoundaryConditions()
    {
        var list = new GapBufferList<int>();

        try
        {
            list.RemoveAt(0);
            Assert(false, "RemoveAt on empty list did not throw");
        }
        catch
        {
        }

        try
        {
            var _ = list[0];
            Assert(false, "Indexer get on empty list did not throw");
        }
        catch
        {
        }

        try
        {
            list.Insert(1, 5);
            Assert(false, "Insert out of range did not throw");
        }
        catch
        {
        }
    }

    private static void Test_RandomizedAgainstList()
    {
        var rnd = new System.Random();

        var gb = new GapBufferList<int>();
        var refList = new List<int>();

        for (var i = 0; i < 500; i++)
        {
            var op = rnd.Next(4);

            switch (op)
            {
                case 0: // Add
                {
                    var v = rnd.Next(1000);
                    gb.Add(v);
                    refList.Add(v);
                    PerformAssert();
                    break;
                }
                case 1: // Insert
                {
                    if (refList.Count == 0) break;
                    var idx = rnd.Next(refList.Count);
                    var v = rnd.Next(1000);
                    gb.Insert(idx, v);
                    refList.Insert(idx, v);
                    PerformAssert();
                    break;
                }
                case 2: // RemoveAt
                {
                    if (refList.Count == 0) break;
                    var idx = rnd.Next(refList.Count);
                    gb.RemoveAt(idx);
                    refList.RemoveAt(idx);
                    PerformAssert();
                    break;
                }
                case 3: // Set
                {
                    if (refList.Count == 0) break;
                    var idx = rnd.Next(refList.Count);
                    var v = rnd.Next(1000);
                    gb[idx] = v;
                    refList[idx] = v;
                    PerformAssert();
                    break;
                }
            }
        }

        return;

        void PerformAssert()
        {
            Assert(gb.ToImmutableStableList().Equals(refList.ToImmutableStableList()),
                $"\n{refList.ToImmutableStableList()} vs\n{gb.ToImmutableStableList()}");
        }
    }

    private static void Test_Swap()
    {
        var list = new GapBufferList<int> { 0, 1, 2, 3, 4, 5 };

        list.Swap(1, 2, 4, 2);
        AssertSequence(list, IImmutableStableList.Create(0, 4, 5, 3, 1, 2), "Test_Swap");
    }

    private static void Test_Swap_DifferentSizes()
    {
        var list = new GapBufferList<int> { 0, 1, 2, 3, 4, 5, 6 };

        list.Swap(1, 3, 5, 1);
        AssertSequence(list, IImmutableStableList.Create(0, 5, 4, 1, 2, 3, 6), "Test_Swap_DifferentSizes");
    }
    
    private static void Test_RandomizedSwapAgainstList()
    {
        var rnd = new System.Random();

        var gb = new GapBufferList<int>();
        var refList = new List<int>();

        for (int i = 0; i < 30; i++)
        {
            gb.Add(i);
            refList.Add(i);
        }

        for (int i = 0; i < 300; i++)
        {
            if (refList.Count < 2)
                break;

            var countA = rnd.Next(1, Math.Min(5, refList.Count));
            var countB = rnd.Next(1, Math.Min(5, refList.Count));

            var a = rnd.Next(refList.Count - countA + 1);
            var b = rnd.Next(refList.Count - countB + 1);

            var firstIndex = Math.Min(a, b);
            var firstCount = firstIndex == a ? countA : countB;
            var secondIndex = Math.Max(a, b);
            var secondCount = firstIndex == a ? countB : countA;

            if (firstIndex + firstCount > secondIndex)
                continue;

            gb.Swap(a, countA, b, countB);

            var first = refList.GetRange(firstIndex, firstCount);
            var second = refList.GetRange(secondIndex, secondCount);

            refList.RemoveRange(secondIndex, secondCount);
            refList.RemoveRange(firstIndex, firstCount);

            refList.InsertRange(firstIndex, second);
            refList.InsertRange(
                secondIndex + (secondCount - firstCount),
                first
            );

            Assert(
                gb.ToImmutableStableList().Equals(refList.ToImmutableStableList()),
                $"\nExpected: {refList.ToImmutableStableList()}\nActual:   {gb.ToImmutableStableList()}"
            );
        }
    }
}