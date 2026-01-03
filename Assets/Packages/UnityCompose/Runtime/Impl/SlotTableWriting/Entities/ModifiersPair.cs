using System;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

internal readonly record struct ModifiersPair(
    IModifier? Before,
    IModifier? After
);

internal class ModifiersStatePair : IDisposable
{
    private static readonly NewObjectPool<ModifiersStatePair> _pool = new(() => new ModifiersStatePair());
    
    public static ModifiersStatePair Get() => _pool.Get();
        
    private readonly IMutableState<ModifiersPair> _pair = MutableStateOf(new ModifiersPair(null, null));
    
    public ModifiersPair ToModifiersPair() => _pair.Value;

    public void Update(ModifiersPair pair)
    {
        _pair.Value = pair;
    }

    public void Dispose()
    {
        _pair.Clear();
        _pool.Return(this);
    }
}