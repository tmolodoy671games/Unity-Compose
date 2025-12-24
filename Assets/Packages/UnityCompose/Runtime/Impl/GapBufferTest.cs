using System;
using System.Collections.Generic;
using System.Linq;
using SharpExtensions;
using Sirenix.OdinInspector;
using StableCollections;
using UnityCompose;
using UnityEngine;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl
{
    [DisallowMultipleComponent]
    internal class GapBufferTest : MonoBehaviour
    {
        [Button]
        private void Test()
        {
            GapBufferListTests.RunAll();
            // var list = new GapBufferList<int>();
            // for (var i = 0; i < 10; i++)
            //     list.Add(i);
            // Debug.Log(list);
        }
    }
}

public static class GapBufferListTests
{
    private static int _tests;
    private static int _failures;

    public static void RunAll()
    {
        _tests = 0;
        _failures = 0;

        Debug.Log("=== GapBufferList<T> Tests START ===");

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

        Debug.Log($"=== GapBufferList<T> Tests END === Tests: {_tests}, Failures: {_failures}");
    }

    // ---------------------------------------------------------
    // Helpers
    // ---------------------------------------------------------

    private static void Assert(bool condition, string message)
    {
        _tests++;
        if (!condition)
        {
            _failures++;
            Debug.LogError("[GapBufferListTest FAILED] " + message);
        }
    }

    private static GapBufferList<int> NewIntList(params int[] values)
    {
        var list = new GapBufferList<int>();
        for (int i = 0; i < values.Length; i++)
            list.Add(values[i]);
        return list;
    }

    private static void AssertSequence<T>(IList<T> list, params T[] expected)
    {
        Assert(list.Count == expected.Length,
            $"Count mismatch. Expected {expected.Length}, got {list.Count}");

        int n = Math.Min(list.Count, expected.Length);
        for (int i = 0; i < n; i++)
        {
            Assert(
                EqualityComparer<T>.Default.Equals(list[i], expected[i]),
                $"Element mismatch at {i}. Expected {expected[i]}, got {list[i]}: [{expected.JoinToString()}] vs [{list.JoinToString()}]"
            );
        }
    }

    // ---------------------------------------------------------
    // Tests
    // ---------------------------------------------------------

    private static void Test_EmptyList()
    {
        var list = new GapBufferList<int>();

        Assert(list.Count == 0, "Empty list Count != 0");
        Assert(!list.Contains(0), "Empty list Contains returned true");
    }

    private static void Test_Add()
    {
        var list = new GapBufferList<int>();

        list.Add(1);
        list.Add(2);
        list.Add(3);

        AssertSequence(list, 1, 2, 3);
    }

    private static void Test_IndexerGetSet()
    {
        var list = NewIntList(10, 20, 30);

        Assert(list[1] == 20, "Indexer get failed");

        list[1] = 99;
        AssertSequence(list, 10, 99, 30);
    }

    private static void Test_Insert()
    {
        var list = NewIntList(1, 3, 4);

        list.Insert(1, 2);
        AssertSequence(list, 1, 2, 3, 4);

        list.Insert(0, 0);
        AssertSequence(list, 0, 1, 2, 3, 4);

        list.Insert(list.Count, 5);
        AssertSequence(list, 0, 1, 2, 3, 4, 5);
    }

    private static void Test_RemoveAt()
    {
        var list = NewIntList(0, 1, 2, 3, 4);

        list.RemoveAt(2);
        AssertSequence(list, 0, 1, 3, 4);

        list.RemoveAt(0);
        AssertSequence(list, 1, 3, 4);

        list.RemoveAt(list.Count - 1);
        AssertSequence(list, 1, 3);
    }

    private static void Test_Remove()
    {
        var list = NewIntList(1, 2, 3, 2);

        bool removed = list.Remove(2);
        Assert(removed, "Remove returned false for existing value");
        AssertSequence(list, 1, 3, 2);

        removed = list.Remove(999);
        Assert(!removed, "Remove returned true for non-existing value");
    }

    private static void Test_Clear()
    {
        var list = NewIntList(1, 2, 3);

        list.Clear();
        Assert(list.Count == 0, "Clear did not reset Count");

        list.Add(42);
        AssertSequence(list, 42);
    }

    private static void Test_Contains()
    {
        var list = NewIntList(5, 6, 7);

        Assert(list.Contains(6), "Contains failed for existing value");
        Assert(!list.Contains(10), "Contains returned true for missing value");
    }

    private static void Test_CopyTo()
    {
        var list = NewIntList(1, 2, 3);
        var array = new int[5];

        list.CopyTo(array, 1);

        Assert(array[1] == 1, "CopyTo incorrect at index 1");
        Assert(array[2] == 2, "CopyTo incorrect at index 2");
        Assert(array[3] == 3, "CopyTo incorrect at index 3");
    }

    private static void Test_Enumeration()
    {
        var list = NewIntList(1, 2, 3, 4);

        int sum = 0;
        foreach (var v in list)
            sum += v;

        Assert(sum == 10, "Enumeration produced incorrect values");
    }

    /// <summary>
    /// Stress gap movement: middle inserts/removes
    /// </summary>
    private static void Test_InsertRemovePatterns()
    {
        var list = new GapBufferList<int>();

        for (int i = 0; i < 10; i++)
            list.Add(i);

        list.Insert(5, 100);
        list.Insert(5, 101);
        list.RemoveAt(6);

        AssertSequence(list, 0, 1, 2, 3, 4, 101, 5, 6, 7, 8, 9);
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

    /// <summary>
    /// Compares behavior against List<T> using random ops
    /// This is the most important test for a gap buffer.
    /// </summary>
    private static void Test_RandomizedAgainstList()
    {
        var rnd = new System.Random(12345);

        var gb = new GapBufferList<int>();
        var refList = new List<int>();

        for (int i = 0; i < 500; i++)
        {
            int op = rnd.Next(4);

            switch (op)
            {
                case 0: // Add
                {
                    int v = rnd.Next(1000);
                    gb.Add(v);
                    refList.Add(v);
                    // Debug.Log($"Add({v})\b" + gb);
                    PerformAssert();
                    break;
                }
                case 1: // Insert
                {
                    if (refList.Count == 0) break;
                    int idx = rnd.Next(refList.Count);
                    int v = rnd.Next(1000);
                    gb.Insert(idx, v);
                    refList.Insert(idx, v);
                    // Debug.Log($"Insert({idx}, {v})\b" + gb);
                    PerformAssert();
                    break;
                }
                case 2: // RemoveAt
                {
                    if (refList.Count == 0) break;
                    int idx = rnd.Next(refList.Count);
                    gb.RemoveAt(idx);
                    refList.RemoveAt(idx);
                    // Debug.Log($"RemoveAt({idx})\b" + gb);
                    PerformAssert();
                    break;
                }
                case 3: // Set
                {
                    if (refList.Count == 0) break;
                    int idx = rnd.Next(refList.Count);
                    int v = rnd.Next(1000);
                    gb[idx] = v;
                    refList[idx] = v;
                    // Debug.Log($"Set({idx}, {v})\b" + gb);
                    PerformAssert();
                    break;
                }
            }
        }

        void PerformAssert()
        {
            Assert(gb.ToImmutableStableList().Equals(refList.ToImmutableStableList()),
                $"\n{refList.ToImmutableStableList()} vs\n{gb.ToImmutableStableList()}");
        }
    }
}