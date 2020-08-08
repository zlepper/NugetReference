using System.Collections.Generic;
using NugetReference.Core.Models;

namespace NugetReference.Core.Test
{
    
    /// <summary>
    /// Provides utilities for helping with writing tests that analyses a specific assembly
    /// </summary>
    public abstract class AnalysisTestBase
    {
        /// <summary>
        /// Compiles the specific code and runs the code analyser
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        protected TypeDefinition AnalyseType(string code)
        {
            return AnalyseTypes(code)[0];
        }

        /// <inheritdoc cref="AnalyseType"/>
        protected List<TypeDefinition> AnalyseTypes(string code)
        {
            
            var helper = new GenerationHelper();
            helper.AddCode(code);
            // Make sure the basic types are available
            helper.AddReference<int>();

            var assembly = helper.GetResultingAssembly();
            
            var analyser = new AssemblyAnalyser();
            return analyser.AnalyseAssembly(assembly);
        }
    }
}