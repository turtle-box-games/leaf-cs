using System;
using System.IO;

namespace Leaf.Nodes
{
    /// <summary>
    /// Large floating-point value that can be stored.
    /// Stores a value in the range +/- 1.7 x 10^308 (15 digits).
    /// </summary>
    public class DoubleNode : Node
    {
        /// <summary>
        /// Retrieve the ID for the type of node.
        /// This can be used to identify, serialize, and cast a node to its type.
        /// The value returned by this property is <see cref="NodeType.Double"/>.
        /// </summary>
        public override NodeType Type => NodeType.Double;

        /// <summary>
        /// Gets and sets the value of the node.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="value">Value of the node.</param>
        public DoubleNode(double value)
        {
            Value = value;
        }

        /// <summary>
        /// Creates a new node by reading its contents from a stream.
        /// </summary>
        /// <param name="reader">Reader used to pull data from the stream.</param>
        /// <returns>Newly constructed node.</returns>
        internal DoubleNode Read(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes the contents of the node to a stream.
        /// </summary>
        /// <param name="writer">Writer used to put data in the stream.</param>
        internal override void Write(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
