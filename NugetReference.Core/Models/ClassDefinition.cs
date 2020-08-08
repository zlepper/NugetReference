using System.Collections.Generic;

namespace NugetReference.Core.Models
{
    /// <summary>
    /// Describes a specific class
    /// </summary>
    public class ClassDefinition : TypeDefinition
    {
        /// <summary>
        /// If true, then the class is abstract, and cannot be instantiated
        /// </summary>
        public bool IsAbstract { get; }
        /// <summary>
        /// If true, then the class is sealed, and cannot be extended
        /// </summary>
        public bool IsSealed { get; }
        
        /// <summary>
        /// The class this class extends
        /// </summary>
        public ClassDefinition? Extends { get; }
        
        /// <summary>
        /// The interfaces this class implements
        /// </summary>
        public List<InterfaceDefinition> Implements { get; }

        /// <summary>
        /// The members that exists on this type
        /// </summary>
        public List<MemberDefinition> Members { get; set; }

        public ClassDefinition(string name, List<MemberDefinition> members, string? ns, bool isAbstract, bool isSealed, ClassDefinition? extends, List<InterfaceDefinition> implements) : base(name, ns)
        {
            Members = members;
            IsAbstract = isAbstract;
            IsSealed = isSealed;
            Extends = extends;
            Implements = implements;
        }
    }
}