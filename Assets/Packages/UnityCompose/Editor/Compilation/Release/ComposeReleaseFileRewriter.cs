using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Packages.UnityCompose.Editor.Compilation.Release.Extensions;
using SharpExtensions;
using StableCollections;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Packages.UnityCompose.Editor.Compilation.Release;

internal static class ComposeReleaseFileRewriter
{
    public static void Rewrite(IEnumerable<FileInfo> files)
    {
        foreach (var file in files)
            Rewrite(file);
    }

    private static void Rewrite(FileInfo original)
    {
        var originalTree = CSharpSyntaxTree.ParseText(File.ReadAllText(original.FullName));
        var originalComposableMethods = originalTree.GetRoot()
            .DescendantNodes()
            .OfType<ClassDeclarationSyntax>()
            .Where(static it => it.IsPartial())
            .SelectMany(static it => it.DescendantNodes())
            .OfType<MethodDeclarationSyntax>()
            .Where(it => it.ShouldBeCompiled())
            .ToImmutableStableList();
        var newRoot = (CSharpSyntaxNode)originalTree.GetRoot();
        for (var i = 0; i < originalComposableMethods.Count; i++)
        {
            var originalComposableMethod = newRoot
                .DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .Where(static it => it.IsPartial())
                .SelectMany(static it => it.DescendantNodes())
                .OfType<MethodDeclarationSyntax>()
                .Where(it => it.ShouldBeCompiledOrSkipped())
                .ToImmutableStableList()[i];

            var newMethodName = "__" + originalComposableMethod.Identifier.ValueText;
            var arguments = originalComposableMethod.ParameterList.Parameters
                .Select(param => param.Identifier.ValueText)
                .JoinToString();
            var statement = $"{newMethodName}({arguments});";
            if (!originalComposableMethod.IsVoid())
                statement = "return " + statement;
            newRoot = newRoot
                .ReplaceNode(
                    originalComposableMethod,
                    originalComposableMethod
                        .WithExpressionBody(null)
                        .WithSemicolonToken(default)
                        .WithBody(Block())
                        .AddReleaseCompiledAttribute()
                        .WithBody(Block(ParseStatement(statement)))
                        .NormalizeWhitespace(elasticTrivia: true, eol: "\n")
                )
                .NormalizeWhitespace(elasticTrivia: true, eol: "\n");
        }

        newRoot = newRoot.NormalizeWhitespace(elasticTrivia: true, eol: "\n");

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