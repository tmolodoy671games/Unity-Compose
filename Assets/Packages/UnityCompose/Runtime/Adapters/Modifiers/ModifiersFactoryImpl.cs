using System;
using System.Threading.Tasks;
using Compose.Net;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers.Other;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Modifiers;

internal class ModifiersFactoryImpl : IModifiersFactory
{
    public IAlignmentModifiersFactory Alignment { get; } = new AlignmentModifiersFactoryImpl();
    public IAppearanceModifiersFactory Appearance { get; } = new AppearanceModifiersFactoryImpl();
    public ITransformModifiersFactory Transform { get; } = new TransformModifiersFactoryImpl();
    public IInsetsModifiersFactory Insets { get; } = new InsetsModifiersFactoryImpl();
    public ISizeModifiersFactory Size { get; } = new SizeModifiersFactoryImpl();
    public IInteractionModifiersFactory Interaction { get; } = new InteractionModifiersFactoryImpl();
    public IPositionModifiersFactory Position { get; } = new  PositionModifiersFactoryImpl();
    
    public IModifier TestTag(string tag) => new TestTagModifierImpl(tag);
    
    public IModifier PointerInput<T>(T key, Func<IPointerInputScope, Task> body)
    {
        return new PointerInputModifierImpl<T>(key, body);
    }
}