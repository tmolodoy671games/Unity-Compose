#nullable enable
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mono.Cecil;
using Packages.UnityCompose.Editor.Extensions;
using Packages.UnityCompose.Editor.Utils;
using Unity.CompilationPipeline.Common.Diagnostics;
using Unity.CompilationPipeline.Common.ILPostProcessing;

namespace Packages.UnityCompose.Editor.Rewriters;

internal class UnityComposeMethodRewriter : ILPostProcessor
{
    public override ILPostProcessor GetInstance() => this;

    public override bool WillProcess(ICompiledAssembly compiledAssembly)
    {
        return false; // BRUH
        var assembly = compiledAssembly.ToAssemblyDefinition();
        return assembly.MainModule.Types
            .SelectMany(it => it.Methods)
            .Any(it => it.IsComposable());
    }

    public override ILPostProcessResult Process(ICompiledAssembly compiledAssembly)
    {
        var assembly = compiledAssembly.ToAssemblyDefinition();
        var messages = new List<DiagnosticMessage>();

        foreach (var type in assembly.MainModule.Types)
        {
            var composableMethods = type.Methods
                .Where(method => method.IsComposable())
                .ToList();
            foreach (var composableMethod in composableMethods)
            {
                var recompiledMethod = type.Methods
                    .Where(it => it.Name == "__" + composableMethod.Name)
                    .FirstOrDefault(it =>
                        it.Parameters.SequenceEqual(composableMethod.Parameters, new ParameterEqualityComparer())
                    );
                var sequencePoint = composableMethod.DebugInformation.SequencePoints.FirstOrDefault();
                if (recompiledMethod == null)
                {
                    var message = new DiagnosticMessage()
                    {
                        DiagnosticType = DiagnosticType.Warning,
                        MessageData = $"{type.FullName}.{composableMethod.Name} is not marked as partial (or code generation failed)!",
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
            }
        }

        // Debug.Log($"Recompiling {assembly.FullName}");
        using var peStream = new MemoryStream();
        using var pdbStream = new MemoryStream();
        var writeParameters = new WriterParameters
        {
            // WriteSymbols = true,
            // SymbolStream = pdbStream,
        };
        assembly.Write(peStream);

        var newAssembly = new InMemoryAssembly(peStream.ToArray(), pdbStream.ToArray());
        return new ILPostProcessResult(newAssembly, messages);
    }
}