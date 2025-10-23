using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ModifierExtensions
{
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

    public static IModifier Top(
        this IModifier modifier,
        StyleLength top,
        ComposeTransition transition = default
    )
    {
        return modifier.Then(new TopImpl(top, transition));
    }

    public static IModifier Left(
        this IModifier modifier,
        StyleLength left,
        ComposeTransition transition = default
    )
    {
        return modifier.Then(new LeftImpl(left, transition));
    }
}