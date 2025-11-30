using System;

namespace Packages.UnityCompose.Impl.SlotTableModels.Models;

public interface IScopeUpdateScope
{
    void UpdateScope(Action restartCallback);
}