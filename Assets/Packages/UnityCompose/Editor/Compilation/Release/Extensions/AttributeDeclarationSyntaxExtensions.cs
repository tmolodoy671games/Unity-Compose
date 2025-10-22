using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SharpExtensions;
using StableCollections;

namespace Packages.UnityCompose.Editor.Compilation.Release.Extensions;

internal static class AttributeDeclarationSyntaxExtensions
{
    public static bool IsComposable(this AttributeSyntax attribute)
    {
        return attribute.Name.ToString() is "Composable" or "ComposableAttributes";
    }

    public static bool IsReleaseCompiled(this AttributeSyntax attribute)
    {
        return attribute.Name.ToString() is "ReleaseCompiled" or "ReleaseCompiledAttribute";
    }

    public static bool IsCompiled(this AttributeSyntax attribute)
    {
        return attribute.Name.ToString() is "Compiled" or "CompiledAttribute";
    }

    private static string? Name(this AttributeArgumentSyntax argument)
    {
        return argument.NameEquals?.Name.Identifier.Text
               ?? argument.NameColon?.Name.Identifier.Text
               ?? null;
    }
    
    private static bool GetBooleanValue(this AttributeArgumentSyntax argument)
    {
        return argument.Expression switch
        {
            LiteralExpressionSyntax literal when literal.IsKind(SyntaxKind.TrueLiteralExpression) => true,
            LiteralExpressionSyntax literal when literal.IsKind(SyntaxKind.FalseLiteralExpression) => false,
            _ => true
        };
    }
}