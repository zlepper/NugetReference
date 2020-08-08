using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NugetReference.Core.Models;

namespace NugetReference.Core
{
    /// <summary>
    /// Analyses a loaded assembly
    /// </summary>
    public class AssemblyAnalyser
    {
        /// <summary>
        /// Internal cache to avoid rechecking the same type multiple types
        /// </summary>
        private Dictionary<TypeInfo, TypeDefinition> TypeCache = new Dictionary<TypeInfo, TypeDefinition>();
        
        public List<TypeDefinition> AnalyseAssembly(Assembly assembly)
        {
            var definitions = new HashSet<TypeDefinition>();
            
            foreach (var type in assembly.DefinedTypes)
            {
                var definition = AnalyseType(type);
                if (definition == null)
                {
                    Console.WriteLine($"Unknown how to handle type: {type}");
                    continue;
                }
                TypeCache[type] = definition;
                definitions.Add(definition);
            }
            
            return definitions.ToList();
        }

        private ClassDefinition AnalyseClass(TypeInfo t)
        {
            var name = t.Name;
            var members = t.DeclaredMembers.Select(AnalyseMember).ToList();
            return new ClassDefinition(name, members, t.Namespace, false, false, null, new List<InterfaceDefinition>());
        }

        private TypeDefinition? AnalyseType(TypeInfo t)
        {
            return t switch
            {
                var cached when TypeCache.TryGetValue(cached, out var c) => c,
                var v when v.IsValueType => AnalyseValueType(v),
                var c when c.IsClass => AnalyseClass(c),
                var s when s.IsEnum => AnalyseEnum(s),
                var i when i.IsInterface => AnalyseInterface(i),
                _ => null
            };
        }

        private TypeDefinition AnalyseValueType(TypeInfo v)
        {
            Console.WriteLine($"Is value type: {v}");

            if (SimpleTypeReference.TryMatchSimpleTypeReference(v, out var typeReference))
            {
                return typeReference;
            }
            
            return null;
        }

        private MemberDefinition AnalyseMember(MemberInfo m)
        {
            return m switch
            {
                MethodInfo mi => AnalyseMethod(mi),
                FieldInfo fi => AnalyseField(fi),
                PropertyInfo pi => AnalyseProperty(pi),
                ConstructorInfo ci => AnalyseConstructor(ci),
                _ => throw new ArgumentOutOfRangeException(nameof(m), m, "Unknown MemberInfo type")
            };
        }

        private MethodDefinition AnalyseMethod(MethodInfo mi)
        {
            return new MethodDefinition(mi.Name);
        }

        public FieldDefinition AnalyseField(FieldInfo fi)
        {
            var typeInfo = fi.FieldType.GetTypeInfo();
            var type = AnalyseType(typeInfo);
            if (type == null)
            {
                throw new Exception($"Unknown type field type: {typeInfo}");
            }
            return new FieldDefinition(fi.Name, fi.IsInitOnly, type);
        }

        public PropertyDefinition AnalyseProperty(PropertyInfo pi)
        {
            return new PropertyDefinition(pi.Name);
        }

        public ConstructorDefinition AnalyseConstructor(ConstructorInfo ci)
        {
            return new ConstructorDefinition(ci.Name);
        }

        public EnumDefinition AnalyseEnum(TypeInfo e)
        {
            return new EnumDefinition(e.Name, e.Namespace);
        }

        public InterfaceDefinition AnalyseInterface(TypeInfo i)
        {
            return new InterfaceDefinition(i.Name, new List<MemberDefinition>(), i.Namespace, new List<InterfaceDefinition>());
        }
    }
}