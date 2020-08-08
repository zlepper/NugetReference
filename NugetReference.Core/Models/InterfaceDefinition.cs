using System.Collections.Generic;

namespace NugetReference.Core.Models
{
    /// <summary>
    /// Describes a specific interface
    /// </summary>
    public class InterfaceDefinition : TypeDefinition
    {
        /// <summary>
        /// The interfaces this interface extends
        /// </summary>
        public List<InterfaceDefinition> Extends { get; set; }

        /// <summary>
        /// The members that exists on this type
        /// </summary>
        public List<MemberDefinition> Members { get; set; }

        public InterfaceDefinition(string name, List<MemberDefinition> members, string? ns, List<InterfaceDefinition> extends) : base(name, ns)
        {
            Members = members;
            Extends = extends;
        }
    }
}