using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using CodingCompetition.Compiler.Interfaces;
using CodingCompetition.Compiler.Models;

namespace CodingCompetition.Compiler.Services.CSharp
{
	public class CSharpRoslynCompiler : ICodeCompiler
	{
		public Task<CompileResult> RunCode(CompileRequest code)
		{
			// define source code, then parse it (to the type used for compilation)
			SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(@"
                using System;

                namespace RoslynCompileSample
                {
                    public class Writer
                    {
                        public void Write(string message)
                        {
                            Console.WriteLine(message);
                        }
                    }
                }");

			// define other necessary objects for compilation
			string assemblyName = Path.GetRandomFileName();
			MetadataReference[] references = new MetadataReference[]
			{
				MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
				MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location)
			};

			// analyse and generate IL code from syntax tree
			CSharpCompilation compilation = CSharpCompilation.Create(
				assemblyName,
				syntaxTrees: new[] { syntaxTree },
				references: references,
				options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

			using (var ms = new MemoryStream())
			{
				// write IL code into memory
				EmitResult result = compilation.Emit(ms);

				if (!result.Success)
				{
					// handle exceptions
					IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
						diagnostic.IsWarningAsError ||
						diagnostic.Severity == DiagnosticSeverity.Error);

					foreach (Diagnostic diagnostic in failures)
					{
						Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
					}
				}
				else
				{
					// load this 'virtual' DLL so that we can use
					ms.Seek(0, SeekOrigin.Begin);
					Assembly assembly = Assembly.Load(ms.ToArray());

					// create instance of the desired class and call the desired function
					Type type = assembly.GetType("RoslynCompileSample.Writer");
					object obj = Activator.CreateInstance(type);
					type.InvokeMember("Write",
						BindingFlags.Default | BindingFlags.InvokeMethod,
						null,
						obj,
						new object[] { "Hello World" });
				}
			}

			return Task.FromResult(new CompileResult());

		}
	}
}
