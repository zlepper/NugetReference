using System.Linq;
using NugetReference.Core.Models;
using NUnit.Framework;

namespace NugetReference.Core.Test
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class ClassSimpleFieldTypeAnalysisTests : AnalysisTestBase
    {
        public static object[][] TestCases = {
            new object[] {"sbyte", SimpleTypeReference.SByte},
            new object[] {"short", SimpleTypeReference.Short}, 
            new object[] {"int", SimpleTypeReference.Int},
            new object[] {"long", SimpleTypeReference.Long},
            new object[] {"byte", SimpleTypeReference.Byte},
            new object[] {"ushort", SimpleTypeReference.UShort},
            new object[] {"uint", SimpleTypeReference.UInt},
            new object[] {"ulong", SimpleTypeReference.ULong}, 
            new object[] {"char", SimpleTypeReference.Char}, 
            new object[] {"float", SimpleTypeReference.Float}, 
            new object[] {"double", SimpleTypeReference.Double}, 
            new object[] {"decimal", SimpleTypeReference.Decimal}, 
            new object[] {"bool", SimpleTypeReference.Bool}, 
        };
        
        [TestCaseSource(nameof(TestCases))]
        public void BaseAccessTest(string typeName, SimpleTypeReference expectedType)
        {
            var code = $@"public class Foo {{ public {typeName} MyField; }}";

            var result = (ClassDefinition)AnalyseType(code);

            var field = result.Members.OfType<FieldDefinition>().Single();
            
            Assert.That(field.Type, Is.EqualTo(expectedType));
        }
    }
}