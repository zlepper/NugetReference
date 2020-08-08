namespace NugetReference.Core.Models
{
    /// <summary>
    /// Describes a specific property on a class or interface
    /// </summary>
    public class PropertyDefinition : MemberDefinition
    {
        /// <summary>
        /// If this properties defines a getter
        /// </summary>
        public bool HasGetter { get; set; }
        
        /// <summary>
        /// If this property defines a setter
        /// </summary>
        public bool HasSetter { get; set; }

        public PropertyDefinition(string name) : base(name)
        {
        }
    }
}