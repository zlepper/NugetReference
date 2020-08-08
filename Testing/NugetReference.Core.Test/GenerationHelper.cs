using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace NugetReference.Core.Test
{
    /// <summary>
    /// Provides ways of generating an assembly dynamically
    /// </summary>
    public class GenerationHelper
    {
        /// <summary>
        /// Contains a set of assemblies names for the assemblies that has already been added
        /// to this compiliation
        /// </summary>
        private readonly HashSet<string> _alreadyAddedAssemblies = new HashSet<string>();

        private readonly List<MetadataReference> _metadataReferences = new List<MetadataReference>();

        private readonly List<SyntaxTree> _syntaxTrees = new List<SyntaxTree>();


        /// <summary>
        /// Adds a reference to the assembly the specified type is from
        /// </summary>
        public void AddReference<T>()
        {
            AddReference(typeof(T));
        }

        /// <summary>
        /// Adds a reference to the assembly the specified type is from
        /// </summary>
        public void AddReference(Type type)
        {
            AddReference(type.Assembly);
        }

        /// <summary>
        /// Add a reference to the specified assembly and all assemblies it depends on
        /// </summary>
        public void AddReference(Assembly assembly)
        {
            // If the assembly is already added to the compilation, then we don't need 
            // to do anything
            if (_alreadyAddedAssemblies.Contains(assembly.GetName().ToString())) return;

            // Add the assembly to the compilation
            _metadataReferences.Add(MetadataReference.CreateFromFile(assembly.Location));

            // Mark it as added, so we don't add it twice
            _alreadyAddedAssemblies.Add(assembly.GetName().ToString());

            foreach (var assemblyName in assembly.GetReferencedAssemblies())
            {
                // To get the dependencies of an assembly, we need to load it first
                // or use the existing loaded instance if possible
                var ass = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(a => a.GetName().ToString() == assemblyName.ToString());

                if (ass == null)
                {
                    ass = Assembly.Load(assemblyName);
                }

                // Recursively add the assembly and any dependencies it might have
                AddReference(ass);
            }
        }

        /// <summary>
        /// Adds the specified class to the compilation
        /// </summary>
        /// <param name="code"></param>
        public void AddCode(string code)
        {
            var tree = CSharpSyntaxTree.ParseText(code);

            _syntaxTrees.Add(tree);
        }

        /// <summary>
        /// Compile the queued code and get the resulting assembly
        /// </summary>
        /// <returns></returns>
        public Assembly GetResultingAssembly()
        {
            // Create a compilation unit
            var compilation = CSharpCompilation.Create(Path.GetRandomFileName(), _syntaxTrees, _metadataReferences,
                new CSharpCompilationOptions(
                    OutputKind.DynamicallyLinkedLibrary, optimizationLevel: OptimizationLevel.Release));

            // Try to compile
            var stream = new MemoryStream();
            var result = compilation.Emit(stream);

            // If the compilation failed, throw an exception
            if (!result.Success)
            {
                throw new Exception(string.Join("\n", result.Diagnostics.Select(d => d.ToString())));
            }


            // Load the resulting assembly into the process
            return Assembly.Load(stream.ToArray());
        }
    }
}