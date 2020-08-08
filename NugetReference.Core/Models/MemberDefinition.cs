namespace NugetReference.Core.Models
{
    /// <summary>
    /// Describes a specific member
    /// </summary>
    public abstract class MemberDefinition
    {
        /// <summary>
        /// The name of this member
        /// </summary>
        public string Name { get; }

        protected MemberDefinition(string name)
        {
            Name = name;
        }
    }
}