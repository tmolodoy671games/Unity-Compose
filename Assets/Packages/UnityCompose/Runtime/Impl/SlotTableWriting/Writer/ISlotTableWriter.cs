using System;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Writer;

internal interface ISlotTableWriter
{
    bool IsInInvalidationRoot();
    
    bool StartRestartGroup(int key);
    void SkipToGroupEnd();
    IComposeRestartScope? GetRestartScope();
    IComposeRestartScope? RequireRestartScope();
    void EndRestartGroup(int key);
    
    void StartReplaceGroup(int key);
    void EndReplaceGroup(int key);

    void StartReusableGroup<T>(int key) where T : VisualElement, new();
    void EndReusableGroup(int key);
    ReusableComposeNode<T> GetReusableNode<T>() where T : VisualElement, new();
    VisualElement? GetParentVisualElement();
    int GetCurrentElementIndex();
    void WriteVisualElement(ComposeView visualElement);
    void EnterVisualElement(VisualElement visualElement);

    void StartLocalGroup(int key);
    void EndLocalGroup(int key);
    T GetCompositionLocal<T>(ICompositionLocal<T> compositionLocal, Func<T> defaultValueFactory);
    CompositionLocalMap? GetCompositionLocalMap();
    string Format();
    
    void StartMovableGroup<T>(int key, T dataKey);
    void EndMovableGroup(int key);

    Optional<T> Read<T>();
    Optional<T> ReadAsStruct<T>();
    void Write<T>(T value);
    void WriteAsStruct<T>(T value);
    bool ReadAndWrite<T>(T value);
    bool ReadAndWriteAsStruct<T>(T value);
    void IncrementSlotIndex();
    
    void Clear();
}