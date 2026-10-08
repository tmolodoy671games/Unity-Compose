#if UNITY_EDITOR
using System;
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
    
    public static void RetargetClosures(this TypeDefinition type, string generatedName, Document? document)
    {
        if (document == null)
            return;

        var token = "<" + generatedName + ">";
        foreach (var method in type.Methods)
        {
            if (method.Name.IndexOf(token, StringComparison.Ordinal) < 0)
                continue;
            if (!method.DebugInformation.HasSequencePoints)
                continue;

            foreach (var point in method.DebugInformation.SequencePoints)
                point.Document = document;
        }

        foreach (var nested in type.NestedTypes)
            nested.RetargetClosures(generatedName, document);
    }

    public static void CopyBodyFrom(this MethodDefinition target, MethodDefinition source)
    {
        var originalDocument = target.DebugInformation.SequencePoints
            .FirstOrDefault(it => it.Document != null)?.Document;

        var body = target.Body;
        var src = source.Body;
        body.Instructions.Clear();
        body.Variables.Clear();
        body.ExceptionHandlers.Clear();
        body.InitLocals = src.InitLocals;
        body.MaxStackSize = src.MaxStackSize;

        foreach (var variable in src.Variables)
            body.Variables.Add(new VariableDefinition(target.Module.ImportReference(variable.VariableType)));

        var map = new Dictionary<Instruction, Instruction>();
        foreach (var instruction in src.Instructions)
        {
            var clone = Instruction.Create(OpCodes.Nop);
            clone.OpCode = instruction.OpCode;
            clone.Operand = instruction.Operand;
            map.Add(instruction, clone);
            body.Instructions.Add(clone);
        }

        foreach (var instruction in body.Instructions)
        {
            var branch = instruction.Operand as Instruction;
            if (branch != null && map.TryGetValue(branch, out var to))
            {
                instruction.Operand = to;
                continue;
            }

            var branches = instruction.Operand as Instruction[];
            if (branches != null)
            {
                var remapped = new Instruction[branches.Length];
                for (var i = 0; i < branches.Length; i++)
                    remapped[i] = map[branches[i]];
                instruction.Operand = remapped;
                continue;
            }

            var variable = instruction.Operand as VariableDefinition;
            if (variable != null)
            {
                instruction.Operand = body.Variables[variable.Index];
                continue;
            }

            var parameter = instruction.Operand as ParameterDefinition;
            if (parameter != null)
                instruction.Operand = parameter.Index < 0 ? body.ThisParameter : target.Parameters[parameter.Index];
        }

        foreach (var handler in src.ExceptionHandlers)
        {
            body.ExceptionHandlers.Add(new ExceptionHandler(handler.HandlerType)
            {
                TryStart = handler.TryStart != null ? map[handler.TryStart] : null,
                TryEnd = handler.TryEnd != null ? map[handler.TryEnd] : null,
                HandlerStart = handler.HandlerStart != null ? map[handler.HandlerStart] : null,
                HandlerEnd = handler.HandlerEnd != null ? map[handler.HandlerEnd] : null,
                FilterStart = handler.FilterStart != null ? map[handler.FilterStart] : null,
                CatchType = handler.CatchType != null ? target.Module.ImportReference(handler.CatchType) : null,
            });
        }

        var debug = target.DebugInformation;
        debug.SequencePoints.Clear();
        if (originalDocument == null)
            return;

        foreach (var instruction in src.Instructions)
        {
            var point = source.DebugInformation.GetSequencePoint(instruction);
            Instruction clone;
            if (point == null || !map.TryGetValue(instruction, out clone))
                continue;

            debug.SequencePoints.Add(new SequencePoint(clone, originalDocument)
            {
                StartLine = point.StartLine,
                StartColumn = point.StartColumn,
                EndLine = point.EndLine,
                EndColumn = point.EndColumn,
            });
        }
    }
}
#endif