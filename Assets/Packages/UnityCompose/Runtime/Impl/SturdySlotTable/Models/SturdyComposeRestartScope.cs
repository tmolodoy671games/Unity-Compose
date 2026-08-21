using System;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SturdySlotTable.Models;

internal class SturdyComposeRestartScope : IComposeRestartScope
{
    public static SturdyComposeRestartScope Get() => new();

    public SturdyComposeGroup Group { get; set; } = null!;
    public VisualElement? VisualElement { get; set; }
    
    private SturdyComposeRestartScope() {}
    
    public void UpdateScope(Action restartCallback)
    {
        throw new NotImplementedException();
    }

    public void Add(BaseMutableStateImpl state)
    {
        throw new NotImplementedException();
    }

    public void RequestRestart()
    {
        throw new NotImplementedException();
    }

    public void Restart()
    {
        throw new NotImplementedException();
    }
}