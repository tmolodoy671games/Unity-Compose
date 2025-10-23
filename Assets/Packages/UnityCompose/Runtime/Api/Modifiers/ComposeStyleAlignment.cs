using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ModifierExtensions
{
    private class AlignSelfImpl : BaseModifier<AlignSelfImpl>
    {
        private readonly StyleEnum<Align> _alignSelf;

        public AlignSelfImpl(StyleEnum<Align> alignSelf)
        {
            _alignSelf = alignSelf;
        }

        public override void Apply(VisualElement element)
        {
            element.style.alignSelf = _alignSelf;
        }

        public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.AlignSelf);
        }

        public override void Revert(VisualElement element)
        {
            element.style.alignSelf = StyleKeyword.Null;
        }

        protected override bool Equals(AlignSelfImpl other)
        {
            return _alignSelf == other._alignSelf;
        }
    }

    private class FlexGrowImpl : BaseModifier<FlexGrowImpl>
    {
        private readonly StyleFloat _flexGrow;
        private readonly ComposeTransition _transition;

        public FlexGrowImpl(StyleFloat flexGrow, ComposeTransition transition)
        {
            _flexGrow = flexGrow;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.flexGrow = _flexGrow;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "flex-grow");
        }

        public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.FlexGrow);
        }

        public override void Revert(VisualElement element)
        {
            element.style.flexGrow = StyleKeyword.Null;
        }

        protected override bool Equals(FlexGrowImpl other)
        {
            return _flexGrow == other._flexGrow;
        }
    }
        
    private class FlexShrinkImpl : BaseModifier<FlexShrinkImpl>
    {
        private readonly StyleFloat _flexShrink;
        private readonly ComposeTransition _transition;

        public FlexShrinkImpl(StyleFloat flexShrink, ComposeTransition transition)
        {
            _flexShrink = flexShrink;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.flexShrink = _flexShrink;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "flex-shrink");
        }

        public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.FlexShrink);
        }

        public override void Revert(VisualElement element)
        {
        }

        protected override bool Equals(FlexShrinkImpl other)
        {
            return _flexShrink.Equals(other._flexShrink) && Equals(_transition, other._transition);
        }
    }

    private class PositionImpl : BaseModifier<PositionImpl>
    {
        private readonly StyleEnum<Position> _position;

        public PositionImpl(StyleEnum<Position> position)
        {
            _position = position;
        }

        public override void Apply(VisualElement element)
        {
            element.style.position = _position;
        }

        public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.Position);
        }

        public override void Revert(VisualElement element)
        {
            element.style.position = StyleKeyword.Null;
        }

        protected override bool Equals(PositionImpl other)
        {
            return _position == other._position;
        }
    }

    private class TopImpl : BaseModifier<TopImpl>
    {
        private readonly StyleLength _top;
        private readonly ComposeTransition _transition;

        public TopImpl(StyleLength top, ComposeTransition transition)
        {
            _top = top;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.top = _top;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "top");
        }

        public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.Top);
        }

        public override void Revert(VisualElement element)
        {
            element.style.top = StyleKeyword.Null;
        }

        protected override bool Equals(TopImpl other)
        {
            return _top == other._top && Equals(_transition, other._transition);
        }
    }

    private class BottomImpl : BaseModifier<BottomImpl>
    {
        private readonly StyleLength _bottom;
        private readonly ComposeTransition _transition;

        public BottomImpl(StyleLength bottom, ComposeTransition transition)
        {
            _bottom = bottom;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.bottom = _bottom;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "bottom");
        }

        public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.Bottom);
        }

        public override void Revert(VisualElement element)
        {
            element.style.bottom = StyleKeyword.Null;
        }

        protected override bool Equals(BottomImpl other)
        {
            return _bottom == other._bottom && Equals(_transition, other._transition);
        }
    }

    private class LeftImpl : BaseModifier<LeftImpl>
    {
        private readonly StyleLength _left;
        private readonly ComposeTransition _transition;

        public LeftImpl(StyleLength left, ComposeTransition transition)
        {
            _left = left;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.left = _left;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "left");
        }

        public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.Left);
        }

        public override void Revert(VisualElement element)
        {
            element.style.left = StyleKeyword.Null;
        }

        protected override bool Equals(LeftImpl other)
        {
            return _left == other._left && Equals(_transition, other._transition);
        }
    }

    private class RightImpl : BaseModifier<RightImpl>
    {
        private readonly StyleLength _right;
        private readonly ComposeTransition _transition;

        public RightImpl(StyleLength right, ComposeTransition transition)
        {
            _right = right;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.right = _right;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "right");
        }

        public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.Right);
        }

        public override void Revert(VisualElement element)
        {
            element.style.right = StyleKeyword.Null;
        }

        protected override bool Equals(RightImpl other)
        {
            return _right == other._right && Equals(_transition, other._transition);
            ;
        }
    }

    public static IModifier AlignSelf(this IModifier style, StyleEnum<Align> alignSelf)
    {
        return style.Then(new AlignSelfImpl(alignSelf));
    }

    public static IModifier FlexGrow(
        this IModifier style,
        StyleFloat flexGrow,
        ComposeTransition transition = default
    )
    {
        return style.Then(new FlexGrowImpl(flexGrow, transition));
    }

    public static IModifier FlexShrink(
        this IModifier style,
        StyleFloat flexShrink,
        ComposeTransition transition = default
    )
    {
        return style.Then(new FlexShrinkImpl(flexShrink, transition));
    }

    public static IModifier Position(this IModifier style, StyleEnum<Position> position)
    {
        return style.Then(new PositionImpl(position));
    }

    public static IModifier Top(
        this IModifier style,
        StyleLength top,
        ComposeTransition transition = default
    )
    {
        return style.Then(new TopImpl(top, transition));
    }

    public static IModifier Bottom(
        this IModifier style,
        StyleLength bottom,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BottomImpl(bottom, transition));
    }

    public static IModifier Left(
        this IModifier style,
        StyleLength left,
        ComposeTransition transition = default
    )
    {
        return style.Then(new LeftImpl(left, transition));
    }

    public static IModifier Right(
        this IModifier style,
        StyleLength right,
        ComposeTransition transition = default
    )
    {
        return style.Then(new RightImpl(right, transition));
    }
}