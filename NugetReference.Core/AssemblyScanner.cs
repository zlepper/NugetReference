using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using NugetReference.Core.Models;

namespace NugetReference.Core
{
    /// <summary>
    /// The initial entry point for scanning an assembly
    /// </summary>
    public class AssemblyScanner
    {
        /// <summary>
        /// The main method for starting the scanning of an assembly
        /// </summary>
        /// <param name="filename">The filename of the assembly to scan</param>
        /// <returns>A list of all the types in the assembly</returns>
        public List<TypeDefinition> ScanAssembly(string filename)
        {
            var loadContext = new AssemblyLoadContext("Scanner: " + filename, true);

            try
            {
                var assembly = loadContext.LoadFromAssemblyPath(filename);
                var analyser = new AssemblyAnalyser();
                return analyser.AnalyseAssembly(assembly);
            }
            finally
            {
                loadContext.Unload();
            }
        }

    }
}