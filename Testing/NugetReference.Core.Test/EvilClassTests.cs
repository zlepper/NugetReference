using System.IO;
using NugetReference.Core.Models;
using NUnit.Framework;

namespace NugetReference.Core.Test
{
    /// <summary>
    /// Verifies that we don't accidentally trigger code in <see cref="EvilAssembly.EvilClass">EvilClass</see> when scanning
    /// </summary>
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class EvilClassTests
    {
        [Test]
        public void DoesNotActivateStaticConstructors()
        {
            var assemblyPath = Path.GetFullPath("NugetReference.EvilAssembly.dll");
            var scanner = new AssemblyScanner();
            var types = scanner.ScanAssembly(assemblyPath);
            
            Assert.That(types, Has.Exactly(1).Items.TypeOf<ClassDefinition>());

            var c = (ClassDefinition) types[0];
            
            Assert.That(c.Extends, Is.Null);
            Assert.That(c.Implements, Is.Empty);
            Assert.That(c.IsAbstract, Is.False);
            Assert.That(c.IsSealed, Is.False);
            Assert.That(c.Namespace, Is.EqualTo("NugetReference.EvilAssembly"));
            Assert.That(c.Members, Has.Exactly(1).Items.TypeOf<MethodDefinition>());
            
            Assert.That("evil-static-ctor.txt", Does.Not.Exist);
        }
    }
}