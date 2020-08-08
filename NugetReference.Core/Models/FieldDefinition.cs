namespace NugetReference.Core.Models
{
    /// <summary>
    /// Describes a specific field on a class
    /// </summary>
    public class FieldDefinition : MemberDefinition
    {
        /// <summary>
        /// If this field is readonly
        /// </summary>
        public bool Readonly { get; }
        
        /// <summary>
        /// The type of the field
        /// </summary>
        public TypeDefinition Type { get; }

        public FieldDefinition(string name, bool @readonly, TypeDefinition type) : base(name)
        {
            Readonly = @readonly;
            Type = type;
        }
    }
}