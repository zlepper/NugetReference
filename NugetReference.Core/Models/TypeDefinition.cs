using System.Collections.Generic;

namespace NugetReference.Core.Models
{
    /// <summary>
    /// Describes a specific type
    /// </summary>
    public abstract class TypeDefinition
    {
        /// <summary>
        /// The name of the class
        /// </summary>
        /// <example>List&lt;Foo&gt;</example>
        public string Name { get; set; }
        
        /// <summary>
        /// The members that exists on this type
        /// </summary>
        public List<MemberDefinition> Members { get; set; }
        
        /// <summary>
        /// The namespace the type is defined in
        /// </summary>
        public string? Namespace { get; set; }

        protected TypeDefinition(string name, List<MemberDefinition> members, string? ns)
        {
            Name = name;
            Members = members;
            Namespace = ns;
        }
    }
}