using Leaf.Nodes;

namespace Leaf.Serialization
{
    /// <summary>
    /// Serialize and deserialize version 1 nodes.
    /// </summary>
    internal interface IVersion1NodeSerializer
    {
        /// <summary>
        /// Serialize a flag node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        void Write(FlagNode node);

        /// <summary>
        /// Serialize a integer-8 node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        void Write(Int8Node node);

        /// <summary>
        /// Serialize a integer-16 node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        void Write(Int16Node node);

        /// <summary>
        /// Serialize a integer-32 node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        void Write(Int32Node node);

        /// <summary>
        /// Serialize a integer-64 node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        void Write(Int64Node node);

        /// <summary>
        /// Serialize a float-32 node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        void Write(Float32Node node);

        /// <summary>
        /// Serialize a float-64 node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        void Write(Float64Node node);

        /// <summary>
        /// Serialize a string node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        void Write(StringNode node);

        /// <summary>
        /// Serialize a time node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        void Write(TimeNode node);

        /// <summary>
        /// Serialize a UUID node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        void Write(UuidNode node);

        /// <summary>
        /// Serialize a blob node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        void Write(BlobNode node);

        /// <summary>
        /// Serialize a list node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        void Write(ListNode node);

        /// <summary>
        /// Serialize a composite node.
        /// </summary>
        /// <param name="node">Node to serialize.</param>
        void Write(CompositeNode node);

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