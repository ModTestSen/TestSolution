using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Npgsql;

namespace TestSolution.SourceGenerator
{
    [Generator]
    public class SourceGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            IncrementalValueProvider<ImmutableArray<ClassDeclarationSyntax>> pipeline =
                context.SyntaxProvider.CreateSyntaxProvider( // A
                        (node, _) => node is ClassDeclarationSyntax, // B
                        (syntax, _) => (ClassDeclarationSyntax) syntax.Node) // C
                    .Collect(); // D

            context.RegisterSourceOutput(pipeline, (ds, source) =>
            {
                var names1 = new PostgreDataProvider().GetSimpleData();

                var names = new List<string>() {"Test1", "Test2", "Test3"};
                foreach (var className in names1)
                {
                    ds.AddSource($"{className}Map.cs", @"
                     using FluentNHibernate.Mapping;

                    namespace TestSolution.Models.Mappings
                   {
                    public class " + className + @"
                    {
                    public virtual long Id { get; set; }
                    }

                    public class " + className + @"Map: ClassMap<" + className + @">
                    {
                       public " + className + @"Map()
                       {
                        this.Id(x => x.Id);            
                       }
                    }
                 }");
                }
            }); // E
        }
    }
}