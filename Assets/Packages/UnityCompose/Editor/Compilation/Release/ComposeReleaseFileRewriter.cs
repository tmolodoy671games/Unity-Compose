using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Packages.UnityCompose.Editor.Compilation.Release.Extensions;
using StableCollections;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Packages.UnityCompose.Editor.Compilation.Release;

internal static class ComposeReleaseFileRewriter
{
    public static void Rewrite(IEnumerable<ComposeSourcePair> pairs)
    {
        foreach (var pair in pairs)
            Rewrite(pair.Original, pair.Generated);
    }

    private static void Rewrite(FileInfo original, FileInfo generated)
    {
        var originalTree = CSharpSyntaxTree.ParseText(File.ReadAllText(original.FullName));
        var generatedTree = CSharpSyntaxTree.ParseText(File.ReadAllText(generated.FullName));
        var originalComposableMethods = originalTree.GetRoot()
            .DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Where(it => it.ShouldBeCompiled())
            .ToImmutableStableList();
        // var generatedComposableMethods = generatedTree.GetRoot()
        //     .DescendantNodes()
        //     .OfType<MethodDeclarationSyntax>()
        //     .ToImmutableStableList();
        // if (originalComposableMethods.Count != generatedComposableMethods.Count)
        //     Debug.LogWarning($"Invalid methods count in {original.Name}!");
        var newRoot = (CSharpSyntaxNode)originalTree.GetRoot();
        for (var i = 0; i < originalComposableMethods.Count; i++)
        {
            var originalComposableMethod = originalComposableMethods[i];
            // var generatedComposableMethod = generatedComposableMethods[i];

            var newMethodName = "__" + originalComposableMethod.Identifier.ValueText;
            var arguments = originalComposableMethod.ParameterList.Parameters
                .Select(param => Argument(IdentifierName(param.Identifier)))
                .ToArray();
            var invocation = InvocationExpression(
                    IdentifierName(newMethodName))
                .WithArgumentList(ArgumentList(SeparatedList(arguments)));
            var invocationStatement = ExpressionStatement(invocation);
            newRoot = newRoot
                .ReplaceNode(
                    originalComposableMethod,
                    originalComposableMethod
                        .AddReleaseCompiledAttribute()
                        .WithBody(Block(invocationStatement))
                        .NormalizeWhitespace(elasticTrivia: true)
                );
        }

        File.WriteAllText(original.FullName, CSharpSyntaxTree.Create(newRoot).GetText().ToString());
    }

    private static MethodDeclarationSyntax AddReleaseCompiledAttribute(this MethodDeclarationSyntax method)
    {
        var compiledAttribute = Attribute(IdentifierName("ReleaseCompiled"));
        var attributeList = AttributeList(
            SingletonSeparatedList(compiledAttribute)
        );

        return method.AddAttributeLists(attributeList);
    }
}