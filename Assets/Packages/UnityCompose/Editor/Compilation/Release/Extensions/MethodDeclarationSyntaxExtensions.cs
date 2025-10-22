using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SharpExtensions;

namespace Packages.UnityCompose.Editor.Compilation.Release.Extensions;

internal static class MethodDeclarationSyntaxExtensions
{
    private static bool IsComposable(this MethodDeclarationSyntax method)
    {
        return method.AttributeLists
            .SelectMany(it => it.Attributes)
            .Any(it => it.IsComposable());
    }

    public static bool ShouldBeCompiled(this MethodDeclarationSyntax method)
    {
        return method.ReturnsVoid() &&
               method.IsComposable() &&
               !method.IsAbstract() &&
               method.GenerateImplementation();
    }

    private static bool ReturnsVoid(this MethodDeclarationSyntax method)
    {
        return method.ReturnType is PredefinedTypeSyntax predefinedType &&
               predefinedType.Keyword.IsKind(SyntaxKind.VoidKeyword);
    }

    private static bool IsAbstract(this MethodDeclarationSyntax method)
    {
        return method.Modifiers.Any(m => m.IsKind(SyntaxKind.AbstractKeyword));
    }

    private static bool GenerateImplementation(this MethodDeclarationSyntax method)
    {
        return method.AttributeLists
            .SelectMany(it => it.Attributes)
            .None(static it => it.IsReleaseCompiled() || it.IsCompiled());
    }
}