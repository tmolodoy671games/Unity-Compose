// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

namespace UnityCompose;

public static class Alignment
{
    public enum Horizontal
    {
        Left,
        Center,
        Stretch,
        Right,
    }

    public enum Vertical
    {
        Top,
        Center,
        Stretch,
        Bottom,
    }

    public static Horizontal Left => Horizontal.Left;
    public static Horizontal CenterHorizontally => Horizontal.Center;
    public static Horizontal StretchHorizontally => Horizontal.Stretch;
    public static Horizontal Right => Horizontal.Right;

    public static Vertical Top => Vertical.Top;
    public static Vertical CenterVertically => Vertical.Center;
    public static Vertical StretchVertically => Vertical.Stretch;
    public static Vertical Bottom => Vertical.Bottom;
}

internal static class AlignmentHorizontalExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Align ToAlign(this Alignment.Horizontal alignment)
    {
        return alignment switch
        {
            Alignment.Horizontal.Left => Align.FlexStart,
            Alignment.Horizontal.Center => Align.Center,
            Alignment.Horizontal.Stretch => Align.Stretch,
            Alignment.Horizontal.Right => Align.FlexEnd,
            _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Justify ToJustify(this Alignment.Horizontal alignment)
    {
        return alignment switch
        {
            Alignment.Horizontal.Left => Justify.FlexStart,
            Alignment.Horizontal.Center => Justify.Center,
            Alignment.Horizontal.Stretch => Justify.SpaceAround,
            Alignment.Horizontal.Right => Justify.FlexEnd,
            _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null)
        };
    }
}

internal static class AlignmentVerticalExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Align ToAlign(this Alignment.Vertical alignment)
    {
        return alignment switch
        {
            Alignment.Vertical.Top => Align.FlexStart,
            Alignment.Vertical.Center => Align.Center,
            Alignment.Vertical.Stretch => Align.Stretch,
            Alignment.Vertical.Bottom => Align.FlexEnd,
            _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Justify ToJustify(this Alignment.Vertical alignment)
    {
        return alignment switch
        {
            Alignment.Vertical.Top => Justify.FlexStart,
            Alignment.Vertical.Center => Justify.Center,
            Alignment.Vertical.Stretch => Justify.SpaceAround,
            Alignment.Vertical.Bottom => Justify.FlexEnd,
            _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null)
        };
    }
}