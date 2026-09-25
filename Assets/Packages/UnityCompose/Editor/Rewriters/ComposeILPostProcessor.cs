#if UNITY_EDITOR
using System.IO;
using System.Linq;
using Packages.UnityCompose.Editor.Extensions;
using Unity.CompilationPipeline.Common.ILPostProcessing;
using UnityEditor;

namespace Packages.UnityCompose.Editor.Rewriters;

[InitializeOnLoad]
internal class ComposeILPostProcessor : ILPostProcessor
{
    public override ILPostProcessor GetInstance() => this;

    public override bool WillProcess(ICompiledAssembly compiledAssembly)
    {
        return ComposableMethodRewriter.CanPatch(compiledAssembly.ToAssemblyDefinition());
    }

    public override ILPostProcessResult Process(ICompiledAssembly compiledAssembly)
    {
        var assembly = compiledAssembly.ToAssemblyDefinition();
        var messages = ComposableMethodRewriter.Patch(assembly);
        
        using var peStream = new MemoryStream();
        assembly.Write(peStream);
        assembly.Dispose();
        
        var inMemoryAssembly = new InMemoryAssembly(
            peStream.ToArray(),
            compiledAssembly.InMemoryAssembly.PdbData
        );
        
        return new ILPostProcessResult(inMemoryAssembly, messages.ToList());
    }
}
#endif