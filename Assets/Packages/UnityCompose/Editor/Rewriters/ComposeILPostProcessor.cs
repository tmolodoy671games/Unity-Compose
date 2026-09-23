#if UNITY_EDITOR
using System.Linq;
using Packages.UnityCompose.Editor.Extensions;
using Unity.CompilationPipeline.Common.ILPostProcessing;

namespace Packages.UnityCompose.Editor.Rewriters;

// BRUH
// internal class ComposeILPostProcessor : ILPostProcessor
// {
//     public override ILPostProcessor GetInstance() => this;
//
//     public override bool WillProcess(ICompiledAssembly compiledAssembly)
//     {
//         return ComposableMethodRewriter.CanPatch(compiledAssembly.ToAssemblyDefinition());
//     }
//
//     public override ILPostProcessResult Process(ICompiledAssembly compiledAssembly)
//     {
//         var messages = ComposableMethodRewriter.Patch(compiledAssembly.ToAssemblyDefinition());
//         return new ILPostProcessResult(compiledAssembly.InMemoryAssembly, messages.ToList());
//     }
// }
#endif