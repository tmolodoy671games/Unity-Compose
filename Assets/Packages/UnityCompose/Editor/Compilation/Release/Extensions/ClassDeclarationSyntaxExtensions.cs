using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Packages.UnityCompose.Editor.Compilation.Release.Extensions;

internal static class ClassDeclarationSyntaxExtensions
{
    public static bool IsPartial(this ClassDeclarationSyntax classDecl)
    {
        return classDecl.Modifiers.Any(SyntaxKind.PartialKeyword);
    }
}