#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Packages.UnityCompose.Editor.Extensions;
using Unity.CompilationPipeline.Common.Diagnostics;
using Unity.CompilationPipeline.Common.ILPostProcessing;
using UnityEditor;

namespace Packages.UnityCompose.Editor.Rewriters;

[InitializeOnLoad]
internal class ComposeILPostProcessor : ILPostProcessor
{
    public override ILPostProcessor GetInstance() => this;

    public override bool WillProcess(ICompiledAssembly compiledAssembly)
    {
        if (!ReferencesCompose(compiledAssembly))
            return false;

        try
        {
            using var assembly = compiledAssembly.ToAssemblyDefinition(readSymbols: false);
            return ComposableMethodRewriter.CanPatch(assembly);
        }
        catch
        {
            return false;
        }
    }

    public override ILPostProcessResult Process(ICompiledAssembly compiledAssembly)
    {
        var assembly = compiledAssembly.ToAssemblyDefinition(readSymbols: true);
        var messages = ComposableMethodRewriter.Patch(assembly);

        using var peStream = new MemoryStream();
        using var pdbStream = new MemoryStream();
        assembly.Write(peStream, new WriterParameters
        {
            WriteSymbols = true,
            SymbolStream = pdbStream,
            SymbolWriterProvider = new PortablePdbWriterProvider(),
        });
        assembly.Dispose();

        return new ILPostProcessResult(
            new InMemoryAssembly(peStream.ToArray(), pdbStream.ToArray()),
            messages.ToList());
    }

    static bool ReferencesCompose(ICompiledAssembly compiledAssembly)
    {
        foreach (var reference in compiledAssembly.References)
            if (reference.Contains("Compose.Net"))
                return true;
        return compiledAssembly.Name.Contains("Compose.Net");
    }
}
#endif