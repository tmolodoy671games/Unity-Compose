using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ModifierExtensions
{
    private class ScaleImpl : BaseModifier<ScaleImpl>
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

        public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.Scale);
        }

        public override void Revert(VisualElement element)
        {
            element.style.scale = StyleKeyword.Null;
        }

        protected override bool Equals(ScaleImpl other)
        {
            return other._scale == _scale && Equals(_transition, other._transition);
        }
    }

    public static IModifier Scale(
        this IModifier style,
        float scale,
        ComposeTransition transition = default
    )
    {
        return style.Then(new ScaleImpl(Vector2.one * scale, transition));
    }
}