// ReSharper disable CheckNamespace

using System;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

namespace UnityCompose;

public interface IComposer
{
    void Clear();
    
    bool IsRestarted();
    void StartRestartGroup(int key);
    
    void StartLocalGroup(int key);
    CompositionLocalMap RequireCompositionLocalMap();
    void EndLocalGroup(int key);
    
    bool Changed();
    bool Changed<TState>(TState state);
    void Write<TState>(TState state);
    
    T GetCompositionLocal<T>(ICompositionLocal<T> compositionLocal, Func<T> defaultValueFactory);
}