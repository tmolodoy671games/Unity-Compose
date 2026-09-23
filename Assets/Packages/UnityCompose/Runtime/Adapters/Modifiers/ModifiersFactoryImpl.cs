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
    public IPointerModifiersFactory Pointer { get; }
    public IMouseModifiersFactory Mouse { get; }
    public ITransformModifiersFactory Transform { get; }
    public IInsetsModifiersFactory Insets { get; }
    public ISizeModifiersFactory Size { get; }
    public IInteractionModifiersFactory Interaction { get; }
    public ICaptureModifiersFactory Capture { get; }
    public IScrollModifiersFactory Scroll { get; }
    public IClickModifiersFactory Click { get; }
    public IPositionModifiersFactory Position { get; }
}