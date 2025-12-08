using System;
using System.Collections.Generic;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Writer;

internal interface ISlotTableWriter
{
    void StartRestartGroup(int key);
    bool IsInInvalidationRoot();
    Optional<T> GetPreviousState<T>();
    Optional<T> GetPreviousStateAsStruct<T>() where T : struct;
    void UpdatePreviousState<T>(T state);
    void UpdatePreviousStateAsStruct<T>(T state) where T : struct;
    void SkipToGroupEnd();
    ComposeRestartScope? GetRestartScope();
    ComposeRestartScope? RequireRestartScope();
    void EndRestartGroup(int key);
    
    void StartReplaceGroup(int key);
    void EndReplaceGroup(int key);
    void EndReplaceGroup();

    void StartReusableGroup(int key);
    void EndReusableGroup(int key);

    Optional<T> Read<T>();
    Optional<T> ReadAsStruct<T>() where T : struct;
    void Write<T>(T value);
    void WriteAsStruct<T>(T value) where T : struct;
    void IncrementSlotIndex();

    T? GetVisualElement<T>() where T : VisualElement;
    void WriteVisualElement(VisualElement visualElement);
    int GetCurrentElementIndex();
    void EnterVisualElement();

    void StartLocalGroup(int key);
    T GetCompositionLocal<T>(ICompositionLocal<T> compositionLocal, Func<T> defaultValueFactory);
    void SetCompositionLocal(IImmutableStableList<CompositionLocalProvides> providers);
    void EndLocalGroup(int key);
    
    void Clear();
    void ResetTo(int groupIndex, Dictionary<ICompositionLocal, IMutableState<object?>>? compositionLocalMap);
    void ResetTo(AnchorId groupAnchor, Dictionary<ICompositionLocal, IMutableState<object?>>? compositionLocalMap);
    void ResetToOutOfBounds();
}