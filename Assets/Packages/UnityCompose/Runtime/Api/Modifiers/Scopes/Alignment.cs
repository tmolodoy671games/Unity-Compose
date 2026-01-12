// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

namespace UnityCompose;

public class Alignment
{
    public class Vertical : Alignment
    {
        internal Vertical(Align align, Justify justify = Justify.FlexStart) : base(align, justify)
        {
        }
    }
    
    public class Horizontal : Alignment
    {
        internal Horizontal(Align align, Justify justify = Justify.FlexStart) : base(align, justify)
        {
        }
    }
    
    private readonly Align _align;
    private readonly Justify _justify;

    public static readonly Horizontal Left = new(Align.FlexStart);
    public static readonly Horizontal CenterHorizontally = new(Align.Center);
    public static readonly Horizontal Right = new(Align.FlexEnd);
    
    public static readonly Vertical Top = new(Align.FlexStart);
    public static readonly Vertical CenterVertically = new(Align.Center);
    public static readonly Vertical Bottom = new(Align.FlexEnd);
    
    public static readonly Alignment TopLeft = new(Align.FlexStart);
    public static readonly Alignment TopCenter = new(Align.Center);
    public static readonly Alignment TopRight = new(Align.FlexEnd);
    
    public static readonly Alignment CenterLeft = new(Align.FlexStart, Justify.Center);
    public static readonly Alignment Center = new(Align.Center, Justify.Center);
    public static readonly Alignment CenterRight = new(Align.FlexEnd, Justify.Center);
    
    public static readonly Alignment BottomLeft = new(Align.FlexStart, Justify.FlexEnd);
    public static readonly Alignment BottomCenter = new(Align.Center, Justify.FlexEnd);
    public static readonly Alignment BottomRight = new(Align.FlexEnd, Justify.FlexEnd);
    
    private Alignment(Align align, Justify justify = Justify.FlexStart)
    {
        _align = align;
        _justify = justify;
    }
    
    internal Align ToAlign() => _align;
    internal Justify ToJustify() => _justify;
}
