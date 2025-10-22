using StableCollections;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeStyleExtensions
{
    private class PickingModeImpl : ComposeStyle<PickingModeImpl>
    {
        private readonly PickingMode _pickingMode;

        public PickingModeImpl(PickingMode pickingMode)
        {
            _pickingMode = pickingMode;
        }

        public override void Apply(VisualElement element)
        {
            element.pickingMode = _pickingMode;
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.PickingMode);
        }

        public override void Revert(VisualElement element)
        {
            element.pickingMode = UnityEngine.UIElements.PickingMode.Position;
        }

        protected override bool Compare(PickingModeImpl other)
        {
            return _pickingMode == other._pickingMode;
        }
    }

    // public static ComposeStyle PickingMode(this ComposeStyle style, PickingMode pickingMode)
    // {
    //     return style.Then(new PickingModeImpl(pickingMode));
    // }
}