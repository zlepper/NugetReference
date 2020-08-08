using System.Collections.Generic;

namespace NugetReference.Core.Models
{
    public class StructDefinition : TypeDefinition
    {
        public StructDefinition(string name, List<MemberDefinition> members, string? ns) : base(name, members, ns)
        {
        }
    }
}