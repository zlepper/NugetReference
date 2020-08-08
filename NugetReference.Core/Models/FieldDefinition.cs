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
        public bool Readonly { get; set; }

        public FieldDefinition(string name, bool @readonly) : base(name)
        {
            Readonly = @readonly;
        }
    }
}