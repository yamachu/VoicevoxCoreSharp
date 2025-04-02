using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace VoicevoxCoreSharp.Experimental.SourceGenerator
{
    [Generator]
    public class NonBlockingGenerator : IIncrementalGenerator
    {
        private static readonly DiagnosticDescriptor DebugLog = new DiagnosticDescriptor(
            id: "NBG001",
            title: "SourceGenerator Debug Info",
            messageFormat: "{0}",
            category: "NonBlockingGenerator",
            DiagnosticSeverity.Info,
            isEnabledByDefault: true
        );

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var methodDeclarations = context.SyntaxProvider.ForAttributeWithMetadataName(
                "VoicevoxCoreSharp.Experimental.Attribute.NonBlockingAttribute",
                static (_, _) => true,
                static (ctx, _) =>
                {
                    // TODO: Convert to primitive Record type
                    return ctx;
                });

            context.RegisterSourceOutput(methodDeclarations, Execute);
        }

        private static void Execute(SourceProductionContext context, GeneratorAttributeSyntaxContext generatorAttributeSyntaxContext)
        {
            var methodDecl = (MethodDeclarationSyntax)generatorAttributeSyntaxContext.TargetNode;
            var methodSymbol = generatorAttributeSyntaxContext.SemanticModel.GetDeclaredSymbol(methodDecl)!;

            var containingType = methodSymbol.ContainingType;
            var namespaceName = containingType.ContainingNamespace.ToDisplayString();
            var className = containingType.Name;
            var methodName = methodSymbol.Name;
            var returnType = methodSymbol.ReturnType.ToString();

            var taskGenericType = GetTaskGenericType(methodSymbol.ReturnType);
            var isVoidTask = SymbolEqualityComparer.Default.Equals(methodSymbol.ReturnType, taskGenericType);

            var isMultipleReturnValue = taskGenericType.IsTupleType;

            var outParameterCount = isMultipleReturnValue
                ? ((INamedTypeSymbol)taskGenericType).TupleElements.Length
                : isVoidTask
                    ? 0
                    : 1;

            var parameters = methodSymbol.Parameters;

            // 引数が存在し、かつ先頭のパラメータが this 型名 引数名 だった場合
            // つまり、拡張メソッドだった場合
            // 例: public static partial Task<string> AnalyzeAsync(this OpenJtalk openJtalk, string text);
            // 先頭の this 型名 引数名 を除外して、残りの引数を取得する
            var isNonStaticClassMethod = parameters.Length > 0 &&
                                         methodSymbol.IsExtensionMethod;

            var baseParamsDeclaration = string.Join(", ", parameters.Select(p => $"{p.Type} {p.Name}"));
            var paramsDeclaration = isNonStaticClassMethod ? "this " + baseParamsDeclaration : baseParamsDeclaration;
            var paramsUsage = isNonStaticClassMethod ? string.Join(", ", parameters.Skip(1).Select(p => p.Name)) : string.Join(", ", parameters.Select(p => p.Name));

            var targetClassName = className.Substring(0, className.Length - "Extensions".Length);
            var syncMethodName = methodName.Substring(0, methodName.Length - "Async".Length);

            var syncMethodTemplate = isNonStaticClassMethod ? $"{parameters.First().Name}.{syncMethodName}" : $"{targetClassName}.{syncMethodName}";

            var effectiveSyncMethodCall = $"{syncMethodTemplate}({paramsUsage}{(outParameterCount > 0 ? (paramsUsage == "" ? "" : ", ") + string.Join(", ", (new int[outParameterCount]).Select((_, i) => $"out var returnValue{i}")) : "")})";

            var source = $@"
using System.Threading.Tasks;
using VoicevoxCoreSharp.Experimental.Exception;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core;

namespace {namespaceName}
{{
    public static partial class {className}
    {{
        public static partial {returnType} {methodName}({paramsDeclaration})
        {{
            return Task.Run(() =>
            {{
                var resultCode = {effectiveSyncMethodCall};
                if (resultCode != ResultCode.RESULT_OK)
                {{
                    throw new VoicevoxCoreResultException(resultCode);
                }}
                return {(outParameterCount == 0 ? "" : "(" + string.Join(", ", Enumerable.Range(0, outParameterCount).Select(i => $"returnValue{i}")) + ")")};
            }});
        }}
    }}
}}";

            // context.ReportDiagnostic(
            //     Diagnostic.Create(
            //         DebugLog,
            //         methodDecl.GetLocation(),
            //         $"{namespaceName}.{className}.{methodName}() generated successfully."
            //     )
            // );

            context.AddSource($"{className}.{methodName}.g.cs", SourceText.From(source, Encoding.UTF8));
        }

        private static ITypeSymbol GetTaskGenericType(ITypeSymbol returnType)
        {
            if (returnType is INamedTypeSymbol namedType &&
                namedType.IsGenericType &&
                namedType.Name == "Task")
            {
                return namedType.TypeArguments[0];
            }
            return returnType;
        }
    }
}
