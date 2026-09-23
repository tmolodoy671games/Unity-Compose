#if UNITY_EDITOR
using Mono.Cecil;

namespace Packages.UnityCompose.Editor.Extensions;

internal static class ParameterDefinitionExtensions
{
    public static bool StructuralEquals(this ParameterDefinition a, ParameterDefinition b)
    {
        return a.Name == b.Name && a.ParameterType.FullName == b.ParameterType.FullName;
    }
}
#endif
