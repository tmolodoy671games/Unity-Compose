#if UNITY_EDITOR
using System.IO;
using System.Linq;
using Mono.Cecil;
using Packages.UnityCompose.Editor.Extensions;
using Packages.UnityCompose.Editor.Utils;
using StableCollections;
using Unity.CompilationPipeline.Common.Diagnostics;
using Unity.CompilationPipeline.Common.ILPostProcessing;

namespace Packages.UnityCompose.Editor.Rewriters;

internal static class ComposableMethodRewriter
{
    public static bool CanPatch(AssemblyDefinition assembly)
    {
        return assembly.MainModule.Types
            .SelectMany(it => it.Methods)
            .Any(it => it.IsComposable());
    }

    public static IStableList<DiagnosticMessage> Patch(AssemblyDefinition assembly)
    {
        var messages = MutableStableListOf<DiagnosticMessage>();
        if (!CanPatch(assembly))
            return messages;
        foreach (var type in assembly.MainModule.Types)
        {
            var composableMethods = type.Methods
                .Where(method => method.IsComposable())
                .ToList();
            foreach (var composableMethod in composableMethods)
            {
                if (!composableMethod.HasBody)
                    continue;
                var recompiledMethod = type.Methods
                    .Where(it => it.Name == "__" + composableMethod.Name)
                    .FirstOrDefault(it =>
                        it.Parameters.SequenceEqual(composableMethod.Parameters, new ParameterEqualityComparer())
                    );
                var sequencePoint = composableMethod.DebugInformation.SequencePoints.FirstOrDefault();
                if (recompiledMethod == null)
                {
                    var message = new DiagnosticMessage
                    {
                        DiagnosticType = DiagnosticType.Error,
                        MessageData =
                            $"{composableMethod.Name}: {type.FullName} is not marked as partial (or code generation failed)!",
                    };
                    if (sequencePoint != null)
                    {
                        message.File = sequencePoint.Document.Url;
                        message.Line = sequencePoint.StartLine;
                        message.Column = sequencePoint.StartColumn;
                    }

                    messages.Add(message);
                    continue;
                }

                composableMethod.CopyBodyFrom(recompiledMethod);
                messages.Add(new DiagnosticMessage { MessageData = $"Patched {composableMethod.Name}" });
            }
        }

        return messages;
    }
}

#endif