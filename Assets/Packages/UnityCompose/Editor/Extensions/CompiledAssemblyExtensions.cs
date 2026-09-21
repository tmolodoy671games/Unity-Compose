using System.IO;
using Mono.Cecil;
using Unity.CompilationPipeline.Common.ILPostProcessing;

namespace Packages.UnityCompose.Editor.Extensions;

internal static class CompiledAssemblyExtensions
{
    public static AssemblyDefinition ToAssemblyDefinition(this ICompiledAssembly compiledAssembly)
    {
        var resolver = new DefaultAssemblyResolver();

        foreach (var reference in compiledAssembly.References)
        {
            var directory = Path.GetDirectoryName(reference);

            if (!string.IsNullOrEmpty(directory))
                resolver.AddSearchDirectory(directory);
        }

        return AssemblyDefinition.ReadAssembly(
            new MemoryStream(compiledAssembly.InMemoryAssembly.PeData),
            new ReaderParameters
            {
                AssemblyResolver = resolver,
            });
        
        // return AssemblyDefinition.ReadAssembly(
        //     new MemoryStream(compiledAssembly.InMemoryAssembly.PeData)
        // );
    }
}