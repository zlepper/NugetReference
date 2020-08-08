using System.Linq;
using System.Reflection;
using NugetReference.Core.Models;
using NUnit.Framework;

namespace NugetReference.Core.Test
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class ClassAnalysisTests : AnalysisTestBase
    {
        [Test]
        public void ScansEmptyClass()
        {
            //language=C# File
            var code = @"public class Foo {}";

            var result = AnalyseType(code);
            
            Assert.That(result, Is.TypeOf<ClassDefinition>());
            var cd = (ClassDefinition)result;
            Assert.That(cd.Extends, Is.Null);
            Assert.That(cd.Implements, Is.Empty);
            Assert.That(cd.IsAbstract, Is.False);
            Assert.That(cd.IsSealed, Is.False);
            Assert.That(cd.Members, Has.Exactly(1).Items.TypeOf<ConstructorDefinition>());
            Assert.That(cd.Name, Is.EqualTo("Foo"));
            Assert.That(cd.Namespace, Is.Null);
        }

        [Test]
        public void ScansEmptyClassInNamespace()
        {
            //language=C# File
            var code = @"namespace MyNs { public class Foo {}}";
            
            
            var result = AnalyseType(code);
            
            Assert.That(result, Is.TypeOf<ClassDefinition>());
            var cd = (ClassDefinition)result;
            Assert.That(cd.Extends, Is.Null);
            Assert.That(cd.Implements, Is.Empty);
            Assert.That(cd.IsAbstract, Is.False);
            Assert.That(cd.IsSealed, Is.False);
            Assert.That(cd.Members, Has.Exactly(1).Items.TypeOf<ConstructorDefinition>());
            Assert.That(cd.Name, Is.EqualTo("Foo"));
            Assert.That(cd.Namespace, Is.EqualTo("MyNs"));
        }

        [Test]
        public void ScansSimpleField()
        {
            //language=C# File
            var code = @"public class Foo { public int MyField; }";

            var result = (ClassDefinition) AnalyseType(code);
            
            Assert.That(result.Members, Has.Exactly(2).Items);
            var fields = result.Members.OfType<FieldDefinition>().ToList();
            Assert.That(fields, Has.Exactly(1).Items);
            var field = fields[0];
            Assert.That(field.Readonly, Is.False);
            
            Assert.That(field.Name, Is.EqualTo("MyField"));
            Assert.That(field.Type, Is.EqualTo(SimpleTypeReference.Int));
        }

        [Test]
        public void ScansReadonlyField()
        {
            
            //language=C# File
            var code = @"public class Foo { public readonly int MyField; }";

            var result = (ClassDefinition) AnalyseType(code);
            
            Assert.That(result.Members, Has.Exactly(2).Items);
            var fields = result.Members.OfType<FieldDefinition>().ToList();
            Assert.That(fields, Has.Exactly(1).Items);
            var field = fields[0];
            Assert.That(field.Readonly, Is.True);
            Assert.That(field.Type, Is.EqualTo(SimpleTypeReference.Int));
        }

    }
}