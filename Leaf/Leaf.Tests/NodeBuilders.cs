using System;
using System.Collections.Generic;
using Leaf.Nodes;
using NUnit.Framework.Internal;

namespace Leaf.Tests
{
    /// <summary>
    /// Methods for constructing randomized nodes.
    /// </summary>
    public static class NodeBuilders
    {
        /// <summary>
        /// Picks a random (valid) node type.
        /// </summary>
        /// <param name="randomizer">Testing randomizer.</param>
        /// <returns>Random node type.</returns>
        public static NodeType NextNodeType(this Randomizer randomizer)
        {
            var type = NodeType.End;
            while (type == NodeType.End)
                type = randomizer.NextEnum<NodeType>();
            return type;
        }
        
        /// <summary>
        /// Generates a random node.
        /// </summary>
        /// <param name="randomizer">Testing randomizer.</param>
        /// <returns>Random node.</returns>
        public static Node NextNode(this Randomizer randomizer)
        {
            var type = randomizer.NextNodeType();
            return NextNodeOfType(randomizer, type);
        }

        /// <summary>
        /// Generates a random node of a specific type.
        /// </summary>
        /// <param name="randomizer">Testing randomizer.</param>
        /// <param name="type">Type of node to generate.</param>
        /// <returns>Random node.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Type specified <paramref name="type"/> is invalid.</exception>
        public static Node NextNodeOfType(this Randomizer randomizer, NodeType type)
        {
            var builder = GetBuilderForType(type);
            return builder(randomizer);
        }

        /// <summary>
        /// Generates a random <see cref="FlagNode"/>.
        /// </summary>
        /// <param name="randomizer">Testing randomizer.</param>
        /// <returns>A <see cref="FlagNode"/> with a random value.</returns>
        public static FlagNode NextFlagNode(this Randomizer randomizer)
        {
            var value = randomizer.NextBool();
            return new FlagNode(value);
        }

        /// <summary>
        /// Generates a random <see cref="Int8Node"/>.
        /// </summary>
        /// <param name="randomizer">Testing randomizer.</param>
        /// <returns>A <see cref="Int8Node"/> with a random value.</returns>
        public static Int8Node NextInt8Node(this Randomizer randomizer)
        {
            var value = randomizer.NextByte();
            return new Int8Node(value);
        }

        /// <summary>
        /// Generates a random <see cref="Int16Node"/>.
        /// </summary>
        /// <param name="randomizer">Testing randomizer.</param>
        /// <returns>A <see cref="Int16Node"/> with a random value.</returns>
        public static Int16Node NextInt16Node(this Randomizer randomizer)
        {
            var value = randomizer.NextShort();
            return new Int16Node(value);
        }

        /// <summary>
        /// Generates a random <see cref="Int32Node"/>.
        /// </summary>
        /// <param name="randomizer">Testing randomizer.</param>
        /// <returns>A <see cref="Int32Node"/> with a random value.</returns>
        public static Int32Node NextInt32Node(this Randomizer randomizer)
        {
            var value = randomizer.Next();
            return new Int32Node(value);
        }

        /// <summary>
        /// Generates a random <see cref="Int64Node"/>.
        /// </summary>
        /// <param name="randomizer">Testing randomizer.</param>
        /// <returns>A <see cref="Int64Node"/> with a random value.</returns>
        public static Int64Node NextInt64Node(this Randomizer randomizer)
        {
            var value = randomizer.NextLong();
            return new Int64Node(value);
        }

        /// <summary>
        /// Generates a random <see cref="Float32Node"/>.
        /// </summary>
        /// <param name="randomizer">Testing randomizer.</param>
        /// <returns>A <see cref="Float32Node"/> with a random value.</returns>
        public static Float32Node NextFloat32Node(this Randomizer randomizer)
        {
            var value = randomizer.NextFloat();
            return new Float32Node(value);
        }

        /// <summary>
        /// Generates a random <see cref="Float64Node"/>.
        /// </summary>
        /// <param name="randomizer">Testing randomizer.</param>
        /// <returns>A <see cref="Float64Node"/> with a random value.</returns>
        public static Float64Node NextFloat64Node(this Randomizer randomizer)
        {
            var value = randomizer.NextDouble();
            return new Float64Node(value);
        }

        /// <summary>
        /// Generates a random <see cref="StringNode"/>.
        /// </summary>
        /// <param name="randomizer">Testing randomizer.</param>
        /// <returns>A <see cref="StringNode"/> with a random value.</returns>
        public static StringNode NextStringNode(this Randomizer randomizer)
        {
            var value = randomizer.GetString();
            return new StringNode(value);
        }

        /// <summary>
        /// Generates a random <see cref="TimeNode"/>.
        /// </summary>
        /// <param name="randomizer">Testing randomizer.</param>
        /// <returns>A <see cref="TimeNode"/> with a random value.</returns>
        public static TimeNode NextTimeNode(this Randomizer randomizer)
        {
            var value = randomizer.NextDateTime();
            return new TimeNode(value);
        }

        /// <summary>
        /// Generates a random <see cref="UuidNode"/>.
        /// </summary>
        /// <param name="randomizer">Testing randomizer.</param>
        /// <returns>A <see cref="UuidNode"/> with a random value.</returns>
        public static UuidNode NextUuidNode(this Randomizer randomizer)
        {
            var value = randomizer.NextGuid();
            return new UuidNode(value);
        }

        /// <summary>
        /// Generates a random <see cref="BlobNode"/>.
        /// </summary>
        /// <param name="randomizer">Testing randomizer.</param>
        /// <returns>A <see cref="BlobNode"/> with random contents.</returns>
        public static BlobNode NextBlobNode(this Randomizer randomizer)
        {
            var bytes = randomizer.NextBytes();
            return new BlobNode(bytes);
        }

        /// <summary>
        /// Generates a random <see cref="ListNode"/>.
        /// </summary>
        /// <param name="randomizer">Testing randomizer.</param>
        /// <returns>A <see cref="ListNode"/> with random contents.</returns>
        public static ListNode NextListNode(this Randomizer randomizer)
        {
            var elementType = randomizer.NextNodeType();
            return NextListNodeOfType(randomizer, elementType);
        }

        /// <summary>
        /// Generates a random <see cref="ListNode"/> with elements of a specified type.
        /// </summary>
        /// <param name="randomizer">Testing randomizer.</param>
        /// <param name="elementType">Type of nodes in the list.</param>
        /// <returns>A <see cref="ListNode"/> with random contents.</returns>
        public static ListNode NextListNodeOfType(this Randomizer randomizer, NodeType elementType)
        {
            var elements = GenerateMultipleOfType(randomizer, elementType);
            return new ListNode(elementType, elements);
        }

        /// <summary>
        /// Generates a random <see cref="CompositeNode"/>.
        /// </summary>
        /// <param name="randomizer">Testing randomizer.</param>
        /// <returns>A <see cref="CompositeNode"/> with random contents.</returns>
        public static CompositeNode NextCompositeNode(this Randomizer randomizer)
        {
            var pairs = GenerateNamedNodes(randomizer);
            return new CompositeNode(pairs);
        }

        private static IEnumerable<KeyValuePair<string, Node>> GenerateNamedNodes(Randomizer randomizer)
        {
            var count = randomizer.Next(5, 20);
            for (var i = 0; i < count; ++i)
            {
                var name = randomizer.GetString();
                var node = NextNode(randomizer);
                yield return new KeyValuePair<string, Node>(name, node);
            }
        }

        private static IEnumerable<Node> GenerateMultipleOfType(Randomizer randomizer, NodeType type)
        {
            var count   = randomizer.Next(5, 20);
            var builder = GetBuilderForType(type);
            for (var i = 0; i < count; ++i)
                yield return builder(randomizer);
        }

        private delegate Node NodeBuilder(Randomizer randomizer);

        private static NodeBuilder GetBuilderForType(NodeType type)
        {
            switch (type)
            {
                case NodeType.Flag:
                    return NextFlagNode;
                case NodeType.Int8:
                    return NextInt8Node;
                case NodeType.Int16:
                    return NextInt16Node;
                case NodeType.Int32:
                    return NextInt32Node;
                case NodeType.Int64:
                    return NextInt64Node;
                case NodeType.Float32:
                    return NextFloat32Node;
                case NodeType.Float64:
                    return NextFloat64Node;
                case NodeType.String:
                    return NextStringNode;
                case NodeType.Time:
                    return NextTimeNode;
                case NodeType.Uuid:
                    return NextUuidNode;
                case NodeType.Blob:
                    return NextBlobNode;
                case NodeType.List:
                    return NextListNode;
                case NodeType.Composite:
                    return NextCompositeNode;
                case NodeType.End:
                    throw new ArgumentException("End cannot be used as a type", nameof(type));
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Unknown node type");
            }
        }
    }
}