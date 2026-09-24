using System;
using Compose.Net;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers.Implementations;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers;

internal class ModifiersFactoryImpl : IModifiersFactory
{
    public IModifier TestTag(string tag) => new TestTagModifierImpl(tag);

    public IModifier Custom(object? key, Action<IReusableComposeNode> apply, Action<IReusableComposeNode> revert)
    {
        return new CustomModifierImpl(apply, revert);
    }

    public IAlignmentModifiersFactory Alignment { get; } = new AlignmentModifiersFactoryImpl();
    public IAppearanceModifiersFactory Appearance { get; } = new AppearanceModifiersFactoryImpl();
    public IPointerModifiersFactory Pointer { get; } = new PointerModifiersFactoryImpl();
    public IMouseModifiersFactory Mouse { get; } = new MouseModifiersFactoryImpl();
    public ITransformModifiersFactory Transform { get; } = new TransformModifiersFactoryImpl();
    public IInsetsModifiersFactory Insets { get; } = new InsetsModifiersFactoryImpl();
    public ISizeModifiersFactory Size { get; } = new SizeModifiersFactoryImpl();
    public IInteractionModifiersFactory Interaction { get; } = new InteractionModifiersFactoryImpl();
    public IScrollModifiersFactory Scroll { get; }
    public IPositionModifiersFactory Position { get; }
}