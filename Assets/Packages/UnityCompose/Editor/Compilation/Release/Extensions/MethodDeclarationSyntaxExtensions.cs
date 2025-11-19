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
        return method.IsComposable() &&
               !method.IsAbstract() &&
               !method.IsCompiled() &&
               method.HasBody() &&
               !method.IsReleaseCompiled();
    }
    
    public static bool ShouldBeCompiledOrSkipped(this MethodDeclarationSyntax method)
    {
        return method.IsComposable() &&
               !method.IsAbstract() &&
               method.HasBody() &&
               !method.IsCompiled();
    }

    public static bool IsVoid(this MethodDeclarationSyntax method)
    {
        return method.ReturnType is PredefinedTypeSyntax predefinedType &&
               predefinedType.Keyword.IsKind(SyntaxKind.VoidKeyword);
    }

    private static bool IsAbstract(this MethodDeclarationSyntax method)
    {
        return method.Modifiers.Any(m => m.IsKind(SyntaxKind.AbstractKeyword));
    }

    private static bool IsCompiled(this MethodDeclarationSyntax method)
    {
        return method.AttributeLists
            .SelectMany(it => it.Attributes)
            .Any(static it => it.IsCompiled());
    }

    private static bool IsReleaseCompiled(this MethodDeclarationSyntax method)
    {
        return method.AttributeLists
            .SelectMany(it => it.Attributes)
            .Any(static it => it.IsReleaseCompiled());
    }
    
    private static bool HasBody(this MethodDeclarationSyntax method)
    {
        return method.Body != null || method.ExpressionBody != null;
    }
}