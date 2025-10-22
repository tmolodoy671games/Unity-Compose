using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeStyleExtensions
{
    private class BackgroundColorImpl : ComposeStyle<BackgroundColorImpl>
    {
        private readonly StyleColor _backgroundColor;
        private readonly ComposeTransition _transition;

        public BackgroundColorImpl(StyleColor backgroundColor, ComposeTransition transition)
        {
            _backgroundColor = backgroundColor;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.backgroundColor = _backgroundColor;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "background-color");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.BackgroundColor);
        }

        public override void Revert(VisualElement element)
        {
            element.style.backgroundColor = StyleKeyword.Null;
        }

        protected override bool Compare(BackgroundColorImpl other)
        {
            return _backgroundColor == other._backgroundColor && Equals(_transition, other._transition);
        }
    }

    private class BackgroundImageImpl : ComposeStyle<BackgroundImageImpl>
    {
        private readonly StyleBackground _backgroundImage;

        public BackgroundImageImpl(StyleBackground backgroundImage)
        {
            _backgroundImage = backgroundImage;
        }

        public override void Apply(VisualElement element)
        {
            element.style.backgroundImage = _backgroundImage;
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.BackgroundImage);
        }

        public override void Revert(VisualElement element)
        {
            element.style.backgroundImage = StyleKeyword.Null;
        }

        protected override bool Compare(BackgroundImageImpl other)
        {
            return _backgroundImage == other._backgroundImage;
        }
    }

    private class VisibilityImpl : ComposeStyle<VisibilityImpl>
    {
        private readonly StyleEnum<Visibility> _visibility;

        public VisibilityImpl(StyleEnum<Visibility> visibility)
        {
            _visibility = visibility;
        }

        public override void Apply(VisualElement element)
        {
            element.style.visibility = _visibility;
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.Visibility);
        }

        public override void Revert(VisualElement element)
        {
            element.style.visibility = StyleKeyword.Null;
        }

        protected override bool Compare(VisibilityImpl other)
        {
            return _visibility == other._visibility;
        }
    }

    private class DisplayImpl : ComposeStyle<DisplayImpl>
    {
        private readonly StyleEnum<DisplayStyle> _display;

        public DisplayImpl(StyleEnum<DisplayStyle> display)
        {
            _display = display;
        }

        public override void Apply(VisualElement element)
        {
            element.style.display = _display;
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.Display);
        }

        public override void Revert(VisualElement element)
        {
            element.style.display = StyleKeyword.Null;
        }

        protected override bool Compare(DisplayImpl other)
        {
            return _display == other._display;
        }
    }

    private class OpacityImpl : ComposeStyle<OpacityImpl>
    {
        private readonly StyleFloat _opacity;
        private readonly ComposeTransition _transition;

        public OpacityImpl(StyleFloat opacity, ComposeTransition transition)
        {
            _opacity = opacity;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.opacity = _opacity;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "opacity");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.Opacity);
        }

        public override void Revert(VisualElement element)
        {
            element.style.opacity = StyleKeyword.Null;
        }

        protected override bool Compare(OpacityImpl other)
        {
            return other._opacity == _opacity && Equals(_transition, other._transition);
        }
    }

    private class ScaleImpl : ComposeStyle<ScaleImpl>
    {
        private readonly StyleScale _scale;
        private readonly ComposeTransition _transition;

        public ScaleImpl(StyleScale scale, ComposeTransition transition)
        {
            _scale = scale;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.scale = _scale;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "scale");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.Scale);
        }

        public override void Revert(VisualElement element)
        {
            element.style.scale = StyleKeyword.Null;
        }

        protected override bool Compare(ScaleImpl other)
        {
            return other._scale == _scale && Equals(_transition, other._transition);
        }
    }

    private class RotateImpl : ComposeStyle<RotateImpl>
    {
        private readonly StyleRotate _rotate;
        private readonly ComposeTransition _transition;

        public RotateImpl(StyleRotate rotate, ComposeTransition transition)
        {
            _rotate = rotate;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.rotate = _rotate;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "rotate");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.Rotate);
        }

        public override void Revert(VisualElement element)
        {
            element.style.rotate = StyleKeyword.Null;
        }

        protected override bool Compare(RotateImpl other)
        {
            return _rotate == other._rotate && Equals(_transition, other._transition);
        }
    }

    private class OverflowImpl : ComposeStyle<OverflowImpl>
    {
        private readonly StyleEnum<Overflow> _overflow;

        public OverflowImpl(StyleEnum<Overflow> overflow)
        {
            _overflow = overflow;
        }

        public override void Apply(VisualElement element)
        {
            element.style.overflow = _overflow;
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.Overflow);
        }

        public override void Revert(VisualElement element)
        {
            element.style.overflow = StyleKeyword.Null;
        }

        protected override bool Compare(OverflowImpl other)
        {
            return _overflow == other._overflow;
        }
    }

    public static ComposeStyle BackgroundColor(
        this ComposeStyle style,
        StyleColor color,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BackgroundColorImpl(color, transition));
    }

    public static ComposeStyle BackgroundImage(this ComposeStyle style, StyleBackground image)
    {
        return style.Then(new BackgroundImageImpl(image));
    }

    public static ComposeStyle Visibility(this ComposeStyle style, StyleEnum<Visibility> visibility)
    {
        return style.Then(new VisibilityImpl(visibility));
    }

    public static ComposeStyle Display(this ComposeStyle style, StyleEnum<DisplayStyle> display)
    {
        return style.Then(new DisplayImpl(display));
    }

    public static ComposeStyle Opacity(
        this ComposeStyle style,
        StyleFloat opacity,
        ComposeTransition transition = default
    )
    {
        return style.Then(new OpacityImpl(opacity, transition));
    }

    public static ComposeStyle Scale(
        this ComposeStyle style,
        StyleScale scale,
        ComposeTransition transition = default
    )
    {
        return style.Then(new ScaleImpl(scale, transition));
    }
        
    public static ComposeStyle Scale(
        this ComposeStyle style,
        float scale,
        ComposeTransition transition = default
    )
    {
        return style.Then(new ScaleImpl(Vector2.one * scale, transition));
    }

    public static ComposeStyle Rotate(
        this ComposeStyle style,
        StyleRotate rotate,
        ComposeTransition transition = default
    )
    {
        return style.Then(new RotateImpl(rotate, transition));
    }

    public static ComposeStyle Overflow(this ComposeStyle style, StyleEnum<Overflow> overflow)
    {
        return style.Then(new OverflowImpl(overflow));
    }
}