using StableCollections;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ModifierExtensions
{
    private class PickingModeImpl : BaseModifier<PickingModeImpl>
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

        public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.PickingMode);
        }

        public override void Revert(VisualElement element)
        {
            element.pickingMode = UnityEngine.UIElements.PickingMode.Position;
        }

        protected override bool Equals(PickingModeImpl other)
        {
            return _pickingMode == other._pickingMode;
        }
    }

    // public static ComposeStyle PickingMode(this ComposeStyle style, PickingMode pickingMode)
    // {
    //     return style.Then(new PickingModeImpl(pickingMode));
    // }
}