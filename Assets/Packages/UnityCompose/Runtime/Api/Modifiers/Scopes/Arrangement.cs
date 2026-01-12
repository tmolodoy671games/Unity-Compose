// ReSharper disable CheckNamespace

using UnityEngine.UIElements;

namespace UnityCompose;

public class Arrangement
{
    public class Vertical : Arrangement
    {
        internal Vertical(Justify justify) : base(justify)
        {
        }
    }
    
    public class Horizontal : Arrangement
    {
        internal Horizontal(Justify justify) : base(justify)
        {
        }
    }
    
    public class VerticalOrHorizontal : Arrangement
    {
        private readonly Horizontal _horizontal;
        private readonly Vertical _vertical;
        
        internal VerticalOrHorizontal(Justify justify) : base(justify)
        {
            _horizontal = new Horizontal(justify);
            _vertical = new Vertical(justify);
        }

        public static implicit operator Vertical(VerticalOrHorizontal arrangement) => arrangement._vertical;
        public static implicit operator Horizontal(VerticalOrHorizontal arrangement) => arrangement._horizontal;
    }
    
    private readonly Justify _justify;

    public static Vertical Top = new(Justify.FlexStart);
    public static Vertical Bottom = new(Justify.FlexEnd);
    public static Horizontal Left = new(Justify.FlexStart);
    public static Horizontal Right = new(Justify.FlexEnd);
    
    public static VerticalOrHorizontal Center = new(Justify.Center);

    public Arrangement(Justify justify)
    {
        _justify = justify;
    }

    public Justify ToJustify() => _justify;
}
