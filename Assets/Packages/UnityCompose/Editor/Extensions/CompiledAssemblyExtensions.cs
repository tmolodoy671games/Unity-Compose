using System.IO;
using Mono.Cecil;
using Unity.CompilationPipeline.Common.ILPostProcessing;

namespace Packages.UnityCompose.Editor.Extensions;

internal static class CompiledAssemblyExtensions
{
    public static AssemblyDefinition ToAssemblyDefinition(this ICompiledAssembly compiledAssembly)
    {
        return AssemblyDefinition.ReadAssembly(
            new MemoryStream(compiledAssembly.InMemoryAssembly.PeData)
        );
    }
}