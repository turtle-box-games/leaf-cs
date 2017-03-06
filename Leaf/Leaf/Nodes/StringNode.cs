using System;

namespace Leaf.Nodes
{
    /// <summary>
    /// Storage for a sequence of characters.
    /// This type is suitable for short and medium length text.
    /// The text is encoded with UTF-8.
    /// </summary>
    /// <remarks>Strings cannot be <c>null</c>.</remarks>
    public class StringNode : Node
    {
        private string _value;

        /// <summary>
        /// Retrieve the ID for the type of node.
        /// This can be used to identify, serialize, and cast a node to its type.
        /// The value returned by this property is <see cref="NodeType.String"/>.
        /// </summary>
        public override NodeType Type => NodeType.String;

        /// <summary>
        /// Version number this node type was introduced in.
        /// The value returned by this property is 1.
        /// </summary>
        public override int Version => 1;

        /// <summary>
        /// Gets and sets the value of the node.
        /// </summary>
        /// <exception cref="ArgumentNullException">The value being set is <c>null</c>.</exception>
        public string Value
        {
            get { return _value; }
            set
            {
                if(value == null)
                    throw new ArgumentNullException();
                _value = value;
            }
        }

        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="value">Value of the node.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="value"/> is <c>null</c>.</exception>
        public StringNode(string value)
        {
            if(value == null)
                throw new ArgumentNullException(nameof(value));
            _value = value;
        }
    }
}
