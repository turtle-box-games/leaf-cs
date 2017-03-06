using System;

namespace Leaf.Nodes
{
    /// <summary>
    /// Storage for a date and time.
    /// Smallest precision (unit) available is 1 microsecond.
    /// Available date and time range is January 1, 1900 through December 31, 9999.
    /// </summary>
    public class TimeNode : Node
    {
        /// <summary>
        /// Retrieve the ID for the type of node.
        /// This can be used to identify, serialize, and cast a node to its type.
        /// The value returned by this property is <see cref="NodeType.Time"/>.
        /// </summary>
        public override NodeType Type => NodeType.Time;

        /// <summary>
        /// Version number this node type was introduced in.
        /// The value returned by this property is 1.
        /// </summary>
        public override int Version => 1;

        /// <summary>
        /// Gets and sets the value of the node.
        /// </summary>
        public DateTime Value { get; set; }

        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="value">Value of the node.</param>
        public TimeNode(DateTime value)
        {
            Value = value;
        }
    }
}
