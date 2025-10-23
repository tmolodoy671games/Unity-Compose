using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeStyleExtensions
{
    private class WidthImpl : IModifier<WidthImpl>
    {
        private readonly StyleLength _width;
        private readonly bool _respectPadding;

        public WidthImpl(StyleLength width, bool respectPadding)
        {
            _width = width;
            _respectPadding = respectPadding;
        }

        public override void Apply(VisualElement element)
        {
            var style = element.style;
            style.width = ResolveWidth(style);
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.Width);
        }

        public override void Revert(VisualElement element)
        {
            element.style.width = StyleKeyword.Null;
        }

        protected override bool Compare(WidthImpl other)
        {
            return _width == other._width;
        }

        private StyleLength ResolveWidth(IStyle style)
        {
            if (!_respectPadding) return _width;
            var paddingLeft = style.paddingLeft;
            var paddingRight = style.paddingRight;
            if (paddingLeft.value.unit == paddingRight.value.unit && paddingRight.value.unit == _width.value.unit)
            {
                return new Length(
                    paddingLeft.value.value +
                    _width.value.value +
                    paddingRight.value.value,
                    _width.value.unit
                );
            }

            return _width;
        }
    }

    private class HeightImpl : IModifier<HeightImpl>
    {
        private readonly StyleLength _height;
        private readonly bool _respectPadding;

        public HeightImpl(StyleLength height, bool respectPadding)
        {
            _height = height;
            _respectPadding = respectPadding;
        }

        public override void Apply(VisualElement element)
        {
            var style = element.style;
            style.height = ResolveHeight(style);
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.Height);
        }

        public override void Revert(VisualElement element)
        {
            element.style.height = StyleKeyword.Null;
        }

        protected override bool Compare(HeightImpl other)
        {
            return _height == other._height;
        }

        private StyleLength ResolveHeight(IStyle style)
        {
            if (!_respectPadding) return _height;
            var paddingTop = style.paddingTop;
            var paddingBottom = style.paddingBottom;
            if (paddingTop.value.unit == paddingBottom.value.unit && paddingBottom.value.unit == _height.value.unit)
            {
                return new Length(
                    paddingTop.value.value +
                    _height.value.value +
                    paddingBottom.value.value,
                    _height.value.unit
                );
            }

            return _height;
        }
    }

    private class MaxWidthImpl : IModifier<MaxWidthImpl>
    {
        private readonly StyleLength _maxWidth;

        public MaxWidthImpl(StyleLength maxWidth)
        {
            _maxWidth = maxWidth;
        }

        public override void Apply(VisualElement element)
        {
            element.style.maxWidth = _maxWidth;
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.MaxWidth);
        }

        public override void Revert(VisualElement element)
        {
            element.style.maxWidth = StyleKeyword.Null;
        }

        protected override bool Compare(MaxWidthImpl other)
        {
            return _maxWidth == other._maxWidth;
        }
    }

    private class MaxHeightImpl : IModifier<MaxHeightImpl>
    {
        private readonly StyleLength _maxHeight;

        public MaxHeightImpl(StyleLength maxHeight)
        {
            _maxHeight = maxHeight;
        }

        public override void Apply(VisualElement element)
        {
            element.style.maxHeight = _maxHeight;
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.MaxHeight);
        }

        public override void Revert(VisualElement element)
        {
            element.style.maxHeight = StyleKeyword.Null;
        }

        protected override bool Compare(MaxHeightImpl other)
        {
            return _maxHeight == other._maxHeight;
        }
    }

    private class MinWidthImpl : IModifier<MinWidthImpl>
    {
        private readonly StyleLength _width;
        private readonly ComposeTransition _transition;

        public MinWidthImpl(StyleLength width, ComposeTransition transition)
        {
            _width = width;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.minWidth = _width;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "min-width");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.MinWidth);
        }

        public override void Revert(VisualElement element)
        {
        }

        protected override bool Compare(MinWidthImpl other)
        {
            return _width.Equals(other._width) && Equals(_transition, other._transition);
        }
    }

    private class MinHeightImpl : IModifier<MinHeightImpl>
    {
        private readonly StyleLength _height;
        private readonly ComposeTransition _transition;

        public MinHeightImpl(StyleLength height, ComposeTransition transition)
        {
            _height = height;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.minHeight = _height;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "min-width");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.MinWidth);
        }

        public override void Revert(VisualElement element)
        {
        }

        protected override bool Compare(MinHeightImpl other)
        {
            return _height.Equals(other._height) && Equals(_transition, other._transition);
        }
    }

    public static IModifier Width(this IModifier style, StyleLength width, bool respectPadding = false)
    {
        return style.Then(new WidthImpl(width, respectPadding));
    }

    public static IModifier MaxWidth(this IModifier style, StyleLength maxWidth)
    {
        return style + new MaxWidthImpl(maxWidth);
    }

    public static IModifier MinWidth(this IModifier style, StyleLength minWidth,
        ComposeTransition transition = default)
    {
        return style + new MinWidthImpl(minWidth, transition);
    }

    public static IModifier Height(this IModifier style, StyleLength height, bool respectPadding = false)
    {
        return style.Then(new HeightImpl(height, respectPadding));
    }

    public static IModifier MaxHeight(this IModifier style, StyleLength maxHeight)
    {
        return style + new MaxHeightImpl(maxHeight);
    }

    public static IModifier MinHeight(this IModifier style, StyleLength minHeight,
        ComposeTransition transition = default)
    {
        return style + new MinHeightImpl(minHeight, transition);
    }

    public static IModifier Size(this IModifier style, StyleLength width, StyleLength height,
        bool respectPadding = false)
    {
        return style.Then(new WidthImpl(width, respectPadding)).Then(new HeightImpl(height, respectPadding));
    }

    public static IModifier Size(this IModifier style, StyleLength size, bool respectPadding = false)
    {
        return style.Then(new WidthImpl(size, respectPadding)).Then(new HeightImpl(size, respectPadding));
    }

    public static IModifier Size(this IModifier style, Vector2 size, bool respectPadding = false)
    {
        return style.Then(new WidthImpl(size.x, respectPadding)).Then(new HeightImpl(size.y, respectPadding));
    }
}