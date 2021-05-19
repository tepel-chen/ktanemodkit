
namespace KModkit
{
    /// <summary>
    /// Interface for tuple type. Written by Emik.
    /// </summary>
    public interface ITuple
    {
        /// <value>
        /// All of the tuple's items as an array, ordered by item number.
        /// </value>
        object[] ToArray { get; }
    }
}
