using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace NugetReference.Core.Models
{
    /// <summary>
    /// Reference to the simple types
    /// </summary>
    public class SimpleTypeReference : TypeDefinition
    {

        private Type UnderlyingType;
        
        private SimpleTypeReference(string name, Type type) : base(name, type.Namespace!)
        {
            Name = name;
            UnderlyingType = type;
        }
        
        public static readonly SimpleTypeReference SByte = new SimpleTypeReference("sbyte", typeof(sbyte));
        public static readonly SimpleTypeReference Short = new SimpleTypeReference("short", typeof(short));
        public static readonly SimpleTypeReference Int = new SimpleTypeReference("int", typeof(int));
        public static readonly SimpleTypeReference Long = new SimpleTypeReference("long", typeof(long));
        public static readonly SimpleTypeReference Byte = new SimpleTypeReference("byte", typeof(byte));
        public static readonly SimpleTypeReference UShort = new SimpleTypeReference("ushort", typeof(ushort));
        public static readonly SimpleTypeReference UInt = new SimpleTypeReference("uint", typeof(uint));
        public static readonly SimpleTypeReference ULong = new SimpleTypeReference("ulong", typeof(ulong));
        public static readonly SimpleTypeReference Char = new SimpleTypeReference("char", typeof(char));
        public static readonly SimpleTypeReference Float = new SimpleTypeReference("float", typeof(float));
        public static readonly SimpleTypeReference Double = new SimpleTypeReference("double", typeof(double));
        public static readonly SimpleTypeReference Decimal = new SimpleTypeReference("decimal", typeof(decimal));
        public static readonly SimpleTypeReference Bool = new SimpleTypeReference("bool", typeof(bool));

        private static readonly List<SimpleTypeReference> AllSimpleTypes = new List<SimpleTypeReference>
        {
            SByte,
            Short,
            Int,
            Long,
            Byte,
            UShort,
            UInt,
            ULong,
            Char,
            Float,
            Double,
            Decimal,
            Bool
        };

        public static bool TryMatchSimpleTypeReference(TypeInfo ti,
            [MaybeNullWhen(false)] out SimpleTypeReference reference)
        {
            var type = AllSimpleTypes.FirstOrDefault(t => ti == t.UnderlyingType);
            if (type == null)
            {
                reference = null!;
                return false;
            }

            reference = type;
            return true;
        }
    }
}