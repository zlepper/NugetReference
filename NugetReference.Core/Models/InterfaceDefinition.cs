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

        public InterfaceDefinition(string name, List<MemberDefinition> members, string? ns, List<InterfaceDefinition> extends) : base(name, members, ns)
        {
            Extends = extends;
        }
    }
}