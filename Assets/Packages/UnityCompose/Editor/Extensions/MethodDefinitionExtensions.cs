#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Packages.UnityCompose.Editor.Extensions;

internal static class MethodDefinitionExtensions
{
    public static bool IsComposable(this MethodDefinition method)
    {
        return method.HasCustomAttributes && method.CustomAttributes
            .Any(it => it.AttributeType.FullName == "Compose.Net.Composable");
    }

    public static bool IsRecompiled(this MethodDefinition method)
    {
        return method.HasCustomAttributes && method.CustomAttributes
            .Any(it => it.AttributeType.FullName == "Compose.Net.RecompiledComposableCode");
    }

    public static void CopyBodyFrom(this MethodDefinition method, MethodDefinition source)
    {
        // Настоящий документ оригинала (путь так, как его записал Unity).
        // Берём ДО перезаписи тела, пока у метода ещё его собственные sequence points.
        var originalDocument = method.DebugInformation.SequencePoints
            .FirstOrDefault(it => !it.IsHidden)?.Document;

        method.Body.LocalVarToken = source.Body.LocalVarToken;
        method.Body.InitLocals = source.Body.InitLocals;
        method.Body.MaxStackSize = source.Body.MaxStackSize;

        method.Body.Instructions.Clear();
        foreach (var instruction in source.Body.Instructions)
            method.Body.Instructions.Add(instruction);

        method.Body.ExceptionHandlers.Clear();
        foreach (var handler in source.Body.ExceptionHandlers)
            method.Body.ExceptionHandlers.Add(handler);

        method.Body.Variables.Clear();
        foreach (var variable in source.Body.Variables)
            method.Body.Variables.Add(variable);

        CopyDebugInformation(method, source, originalDocument);
    }

    private static void CopyDebugInformation(
        MethodDefinition method,
        MethodDefinition source,
        Document? originalDocument)
    {
        var sourceDebug = source.DebugInformation;
        var methodDebug = method.DebugInformation;

        // Sequence points читаются из PDB по IL-offset'у, а инструкции у нас те же объекты,
        // поэтому находим их по offset'у в теле источника.
        var byOffset = new Dictionary<int, Instruction>();
        foreach (var instruction in source.Body.Instructions)
            byOffset[instruction.Offset] = instruction;

        methodDebug.SequencePoints.Clear();
        foreach (var point in sourceDebug.SequencePoints)
        {
            if (!byOffset.TryGetValue(point.Offset, out var instruction))
                continue;

            // Документ сгенерированного файла заменяем документом оригинала:
            // и "Library/Bee/..." пропадает, и путь тот же, что Unity пишет для обычного кода.
            methodDebug.SequencePoints.Add(new SequencePoint(instruction, originalDocument ?? point.Document)
            {
                StartLine = point.StartLine,
                StartColumn = point.StartColumn,
                EndLine = point.EndLine,
                EndColumn = point.EndColumn,
            });
        }

        // Имена и области видимости локальных переменных
        methodDebug.Scope = sourceDebug.Scope;
    }
}
#endif