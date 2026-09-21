using System;
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
        var assembly = compiledAssembly.ToAssemblyDefinition();
        return assembly.MainModule.Types
            .SelectMany(it => it.Methods)
            .Any(it => it.IsComposable());
    }

    public override ILPostProcessResult Process(ICompiledAssembly compiledAssembly)
    {
        var assembly = compiledAssembly.ToAssemblyDefinition();
        var messages = new List<DiagnosticMessage>();

        // foreach (var type in assembly.MainModule.Types)
        // {
        //     var composableMethods = type.Methods
        //         .Where(method => method.IsComposable())
        //         .ToList();
        //     foreach (var composableMethod in composableMethods)
        //     {
        //         var recompiledMethod = type.Methods
        //             .Where(it => it.Name == "__" + composableMethod.Name)
        //             .FirstOrDefault(it =>
        //                 it.Parameters.SequenceEqual(composableMethod.Parameters, new ParameterEqualityComparer())
        //             );
        //         if (recompiledMethod == null)
        //         {
        //             messages.Add(new DiagnosticMessage()
        //             {
        //                 DiagnosticType = DiagnosticType.Error,
        //                 MessageData = $"{type.FullName} is not marked as partial!"
        //             });
        //             continue;
        //         }
        //
        //         composableMethod.CopyBodyFrom(recompiledMethod);
        //     }
        // }

        using var peStream = new MemoryStream();
        using var pdbStream = new MemoryStream();
        var writeParameters = new WriterParameters
        {
            WriteSymbols = true,
            SymbolStream = peStream,
        };
        // assembly.Write(peStream, writeParameters);

        var newAssembly = new InMemoryAssembly(peStream.ToArray(), pdbStream.ToArray());
        return new ILPostProcessResult(newAssembly, messages);
    }
}