using System.Collections.Generic;
using Mono.Cecil;
using Packages.UnityCompose.Editor.Extensions;

namespace Packages.UnityCompose.Editor.Utils;

internal class ParameterEqualityComparer : IEqualityComparer<ParameterDefinition>
{
    public bool Equals(ParameterDefinition x, ParameterDefinition y)
    {
        return x.StructuralEquals(y);
    }

    public int GetHashCode(ParameterDefinition obj)
    {
        return obj.Name.GetHashCode() * 31 +
               obj.ParameterType.FullName.GetHashCode();
    }
}