namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

internal readonly record struct ModifiersPair(
    IModifier? Before,
    IModifier? After
);

internal class ModifiersStatePair
{
    private readonly IMutableState<ModifiersPair> _pair = MutableStateOf(new ModifiersPair(null, null));

    public ModifiersPair ToModifiersPair() => _pair.Value;

    public void Update(ModifiersPair pair)
    {
        _pair.Value = pair;
    }
}