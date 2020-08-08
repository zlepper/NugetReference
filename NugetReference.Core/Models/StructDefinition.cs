using System.Collections.Generic;

namespace NugetReference.Core.Models
{
    public class StructDefinition : TypeDefinition
    {
        public StructDefinition(string name, List<MemberDefinition> members, string? ns) : base(name, ns)
        {
            Members = members;
        }

        /// <summary>
        /// The members that exists on this type
        /// </summary>
        public List<MemberDefinition> Members { get; set; }
    }
}