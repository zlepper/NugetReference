using System;
using System.IO;

namespace NugetReference.EvilAssembly
{
    /// <summary>
    /// Used for testing that we don't accidentally run code in the assemblies we download, since that would be bad
    /// </summary>
    public class EvilClass
    {
        public static int Value;
        
        /// <summary>
        /// A static constructor. If we can find the file on dist, then the constructor was run
        /// </summary>
        static EvilClass()
        {
            File.WriteAllText("evil-static-ctor.txt", "Evil Succeeded");
            Value = 42;
        }

        public void DoSomething()
        {
            // Here for testing only
        }
    }
}