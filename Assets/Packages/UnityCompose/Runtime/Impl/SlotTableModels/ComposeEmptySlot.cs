namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;

internal class ComposeEmptySlot
{
    public static readonly ComposeEmptySlot Instance = new();
    
    private ComposeEmptySlot() {}

    public override string ToString() => "EMPTY";
}