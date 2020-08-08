using System.Collections.Generic;

namespace NugetReference.Core.Models
{
    public class EnumDefinition : TypeDefinition
    {
        public EnumDefinition(string name, List<MemberDefinition> members, string? ns) : base(name, members, ns)
        {
        }
    }
}