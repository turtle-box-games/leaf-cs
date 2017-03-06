using System.IO;

namespace Leaf.Serialization
{
    /// <summary>
    /// Responsible for serializing and deserializing node data to a persistent format.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Write node data to a stream.
        /// </summary>
        /// <param name="container">Container holding the root node and associated data
        /// to be written to the stream.</param>
        /// <param name="output">Output stream to write data to.</param>
        void Serialize(Container container, Stream output);

        /// <summary>
        /// Read node data from a stream.
        /// </summary>
        /// <param name="input">Input stream to read data from.</param>
        /// <returns>Container holding the root node and associated data read from the stream.</returns>
        Container Deserialize(Stream input);
    }
}