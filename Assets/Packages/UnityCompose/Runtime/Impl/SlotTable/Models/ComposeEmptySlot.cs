namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;

internal class ComposeEmptySlot
{
    public static readonly ComposeEmptySlot Instance = new();
    
    private ComposeEmptySlot() {}

    public override string ToString() => "EMPTY";
}