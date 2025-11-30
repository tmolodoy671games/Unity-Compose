namespace Packages.UnityCompose.Impl.SlotTable.Models;

public class Anchor
{
    public int Location;

    public Anchor(int location)
    {
        Location = location;
    }

    public bool IsValid => Location != int.MaxValue;

    public override string ToString()
    {
        return $"Anchor({Location})";
    }
}