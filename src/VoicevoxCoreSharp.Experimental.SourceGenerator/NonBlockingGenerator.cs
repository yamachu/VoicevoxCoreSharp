using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}

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

        private record MethodInfo(
            string Namespace,
            string ClassName,
            string MethodName,
            string ReturnType,
            bool IsExtensionMethod,
            int OutParameterCount,
            ImmutableArray<(string Type, string Name)> Parameters
        ) : IEquatable<MethodInfo>;

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var methodInfos = context.SyntaxProvider.ForAttributeWithMetadataName(
                "VoicevoxCoreSharp.Experimental.Attribute.NonBlockingAttribute",
                static (node, _) => node is MethodDeclarationSyntax,
                static (ctx, _) =>
                {
                    var methodDecl = (MethodDeclarationSyntax)ctx.TargetNode;
                    var methodSymbol = ctx.SemanticModel.GetDeclaredSymbol(methodDecl)!;
                    var containingType = methodSymbol.ContainingType;

                    var taskGenericType = GetTaskGenericType(methodSymbol.ReturnType);
                    var isVoidTask = SymbolEqualityComparer.Default.Equals(methodSymbol.ReturnType, taskGenericType);
                    var isMultipleReturnValue = taskGenericType.IsTupleType;

                    var outParameterCount = isMultipleReturnValue
                        ? ((INamedTypeSymbol)taskGenericType).TupleElements.Length
                        : isVoidTask
                            ? 0
                            : 1;

                    return new MethodInfo(
                        containingType.ContainingNamespace.ToDisplayString(),
                        containingType.Name,
                        methodSymbol.Name,
                        methodSymbol.ReturnType.ToString(),
                        methodSymbol.IsExtensionMethod,
                        outParameterCount,
                        methodSymbol.Parameters.Select(p => (p.Type.ToString(), p.Name)).ToImmutableArray()
                    );
                });

            context.RegisterSourceOutput(methodInfos, Execute);
        }

        private static void Execute(SourceProductionContext context, MethodInfo methodInfo)
        {
            var paramsDeclaration = BuildMethodParameters(methodInfo);
            var methodCall = BuildMethodCall(methodInfo);

            var returnStatement = methodInfo.OutParameterCount == 0
                ? ""
                : "(" + string.Join(", ", Enumerable.Range(0, methodInfo.OutParameterCount).Select(i => $"returnValue{i}")) + ")";

            var source = $@"
using System.Threading.Tasks;
using VoicevoxCoreSharp.Experimental.Exception;
using VoicevoxCoreSharp.Core.Enum;
using VoicevoxCoreSharp.Core;

namespace {methodInfo.Namespace}
{{
    public static partial class {methodInfo.ClassName}
    {{
        public static partial {methodInfo.ReturnType} {methodInfo.MethodName}({paramsDeclaration})
        {{
            return Task.Run(() =>
            {{
                var resultCode = {methodCall};
                if (resultCode != ResultCode.RESULT_OK)
                {{
                    throw new VoicevoxCoreResultException(resultCode);
                }}
                return {returnStatement};
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

            context.AddSource($"{methodInfo.ClassName}.{methodInfo.MethodName}.g.cs", SourceText.From(source, Encoding.UTF8));
        }

        private static string BuildMethodParameters(MethodInfo info)
        {
            var baseParams = string.Join(", ", info.Parameters.Select(p => $"{p.Type} {p.Name}"));
            return info.IsExtensionMethod ? "this " + baseParams : baseParams;
        }

        private static string BuildMethodCall(MethodInfo info)
        {
            var paramUsage = info.IsExtensionMethod
                ? string.Join(", ", info.Parameters.Skip(1).Select(p => p.Name))
                : string.Join(", ", info.Parameters.Select(p => p.Name));

            var targetClassName = info.ClassName.Substring(0, info.ClassName.Length - "Extensions".Length);
            var syncMethodName = info.MethodName.Substring(0, info.MethodName.Length - "Async".Length);
            var methodTemplate = info.IsExtensionMethod
                ? $"{info.Parameters[0].Name}.{syncMethodName}"
                : $"{targetClassName}.{syncMethodName}";

            var outParams = info.OutParameterCount > 0
                ? (paramUsage == "" ? "" : ", ") + string.Join(", ", Enumerable.Range(0, info.OutParameterCount).Select(i => $"out var returnValue{i}"))
                : "";

            return $"{methodTemplate}({paramUsage}{outParams})";
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
