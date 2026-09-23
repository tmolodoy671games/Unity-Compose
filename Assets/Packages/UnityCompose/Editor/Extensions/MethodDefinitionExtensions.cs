#if UNITY_EDITOR
using System.Linq;
using Mono.Cecil;

namespace Packages.UnityCompose.Editor.Extensions;

internal static class MethodDefinitionExtensions
{
    public static bool IsComposable(this MethodDefinition method)
    {
        return method.HasCustomAttributes && method.CustomAttributes
            .Any(it => it.AttributeType.FullName == "Compose.Net.Composable");
    }

    public static void CopyBodyFrom(this MethodDefinition method, MethodDefinition source)
    {
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
    }
}
#endif