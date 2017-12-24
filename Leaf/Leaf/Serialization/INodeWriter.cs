using Leaf.Nodes;

namespace Leaf.Serialization
{
    /// <summary>
    /// Redirection to latest node serializer.
    /// This interface is used so that nodes can reference this interface instead of the latest,
    /// which would change every time there's a new version.
    /// </summary>
    internal interface INodeWriter : IVersion1NodeWriter
    {
        /// <summary>
        /// Redirects to the correct serialization method.
        /// </summary>
        /// <param name="node">Node type to serialize.</param>
        void WriteNode(Node node);
    }
}