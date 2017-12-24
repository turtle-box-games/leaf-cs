using Leaf.Nodes;

namespace Leaf.Serialization
{
    /// <summary>
    /// Capability to deserialize all version 1 node types.
    /// </summary>
    internal interface IVersion1NodeReader
    {
        /// <summary>
        /// Deserialize a flag node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        FlagNode ReadFlagNode();

        /// <summary>
        /// Deserialize a integer-8 node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        Int8Node ReadInt8Node();

        /// <summary>
        /// Deserialize a integer-16 node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        Int16Node ReadInt16Node();

        /// <summary>
        /// Deserialize a integer-32 node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        Int32Node ReadInt32Node();

        /// <summary>
        /// Deserialize a integer-64 node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        Int64Node ReadInt64Node();

        /// <summary>
        /// Deserialize a float-32 node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        Float32Node ReadFloat32Node();

        /// <summary>
        /// Deserialize a float-64 node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        Float64Node ReadFloat64Node();

        /// <summary>
        /// Deserialize a string node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        StringNode ReadStringNode();

        /// <summary>
        /// Deserialize a time node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        TimeNode ReadTimeNode();

        /// <summary>
        /// Deserialize a UUID node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        UuidNode ReadUuidNode();

        /// <summary>
        /// Deserialize a blob node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        BlobNode ReadBlobNode();

        /// <summary>
        /// Deserialize a list node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        ListNode ReadListNode();

        /// <summary>
        /// Deserialize a composite node.
        /// </summary>
        /// <returns>Node read from data.</returns>
        CompositeNode ReadCompositeNode();
    }
}