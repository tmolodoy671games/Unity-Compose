using System;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public abstract class EnterTransition
{
    internal class Composite : EnterTransition
    {
        private readonly EnterTransition _first;
        private readonly EnterTransition _second;

        public Composite(EnterTransition first, EnterTransition second)
        {
            _first = first;
            _second = second;
        }

        public override ComposeStyle Get(float progress, IResolvedStyle resolvedParentStyle)
        {
            return _first.Get(progress, resolvedParentStyle)
                .Then(_second.Get(progress, resolvedParentStyle));
        }
    }

    internal class SlideIn : EnterTransition
    {
        private readonly SlideDirection _direction;

        public SlideIn(SlideDirection direction)
        {
            _direction = direction;
        }

        public override ComposeStyle Get(float progress, IResolvedStyle resolvedParentStyle)
        {
            return _direction switch
            {
                SlideDirection.Up => ComposeStyle.Empty
                    .Top((1 - progress) * resolvedParentStyle.height + resolvedParentStyle.paddingTop),
                SlideDirection.Down => ComposeStyle.Empty
                    .Top((progress - 1) * resolvedParentStyle.height + resolvedParentStyle.paddingTop),
                SlideDirection.Left => ComposeStyle.Empty
                    .Left((1 - progress) * resolvedParentStyle.width + resolvedParentStyle.paddingLeft),
                SlideDirection.Right => ComposeStyle.Empty
                    .Left((progress - 1) * resolvedParentStyle.width + resolvedParentStyle.paddingLeft),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    internal class FadeIn : EnterTransition
    {
        public override ComposeStyle Get(float progress, IResolvedStyle resolvedParentStyle)
        {
            return ComposeStyle.Empty
                .Opacity(progress);
        }
    }

    internal class EmptyImpl : EnterTransition
    {
        public static readonly EmptyImpl Instance = new();

        private EmptyImpl()
        {
        }

        public override ComposeStyle Get(float progress, IResolvedStyle resolvedParentStyle)
        {
            return ComposeStyle.Empty;
        }
    }

    internal class Custom : EnterTransition
    {
        private readonly Func<float, IResolvedStyle, ComposeStyle> _factory;

        public Custom(Func<float, IResolvedStyle, ComposeStyle> factory)
        {
            _factory = factory;
        }

        public override ComposeStyle Get(float progress, IResolvedStyle resolvedParentStyle)
        {
            return _factory(progress, resolvedParentStyle);
        }
    }
    
    public static EnterTransition Empty => EmptyImpl.Instance;

    public abstract ComposeStyle Get(float progress, IResolvedStyle resolvedParentStyle);

    public static EnterTransition operator +(EnterTransition first, EnterTransition second)
    {
        return new Composite(first, second);
    }
}

public abstract class ExitTransition
{
    internal class Composite : ExitTransition
    {
        private readonly ExitTransition _first;
        private readonly ExitTransition _second;

        public Composite(ExitTransition first, ExitTransition second)
        {
            _first = first;
            _second = second;
        }

        public override ComposeStyle Get(float progress, IResolvedStyle resolvedParentStyle)
        {
            return _first.Get(progress, resolvedParentStyle)
                .Then(_second.Get(progress, resolvedParentStyle));
        }
    }

    internal class SlideOut : ExitTransition
    {
        private readonly SlideDirection _direction;

        public SlideOut(SlideDirection direction)
        {
            _direction = direction;
        }

        public override ComposeStyle Get(float progress, IResolvedStyle resolvedParentStyle)
        {
            return _direction switch
            {
                SlideDirection.Up => ComposeStyle.Empty
                    .Top(-progress * resolvedParentStyle.height + resolvedParentStyle.paddingTop),
                SlideDirection.Down => ComposeStyle.Empty
                    .Top(progress * resolvedParentStyle.height + resolvedParentStyle.paddingTop),
                SlideDirection.Left => ComposeStyle.Empty
                    .Left(-progress * resolvedParentStyle.width + resolvedParentStyle.paddingLeft),
                SlideDirection.Right => ComposeStyle.Empty
                    .Left(progress * resolvedParentStyle.width + resolvedParentStyle.paddingLeft),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    internal class FadeOut : ExitTransition
    {
        public override ComposeStyle Get(float progress, IResolvedStyle resolvedParentStyle)
        {
            return ComposeStyle.Empty
                .Opacity(1 - progress);
        }
    }

    internal class EmptyImpl : ExitTransition
    {
        public static readonly EmptyImpl Instance = new();

        private EmptyImpl()
        {
        }

        public override ComposeStyle Get(float progress, IResolvedStyle resolvedParentStyle)
        {
            return ComposeStyle.Empty
                .Opacity(0f);
        }
    }
    
    internal class Custom : ExitTransition
    {
        private readonly Func<float, IResolvedStyle, ComposeStyle> _factory;

        public Custom(Func<float, IResolvedStyle, ComposeStyle> factory)
        {
            _factory = factory;
        }

        public override ComposeStyle Get(float progress, IResolvedStyle resolvedParentStyle)
        {
            return _factory(progress, resolvedParentStyle);
        }
    }
    
    internal class HideImpl : ExitTransition
    {
        public static readonly HideImpl Instance = new();
        
        private HideImpl() {}
        
        public override ComposeStyle Get(float progress, IResolvedStyle resolvedParentStyle)
        {
            return ComposeStyle.Empty
                .Opacity(0f);
        }
    }
    
    public static ExitTransition Empty => EmptyImpl.Instance;
    public static ExitTransition Hide => HideImpl.Instance;

    public abstract ComposeStyle Get(float progress, IResolvedStyle resolvedParentStyle);

    public static ExitTransition operator +(ExitTransition first, ExitTransition second)
    {
        return new Composite(first, second);
    }
}

public record ContentTransform(
    EnterTransition Enter,
    ExitTransition Exit
)
{
    public static ContentTransform Instant => new(
        Enter: EnterTransition.Empty,
        Exit: ExitTransition.Hide
    );

    public static ContentTransform operator +(ContentTransform first, ContentTransform second)
    {
        return new ContentTransform(
            Enter: first.Enter + second.Enter,
            Exit: first.Exit + second.Exit
        );
    }
}

public enum SlideDirection
{
    Up,
    Down,
    Left,
    Right,
}