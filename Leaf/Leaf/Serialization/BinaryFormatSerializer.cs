using System;
using System.IO;

namespace Leaf.Serialization
{
    /// <summary>
    /// Serializes node data to a compact, non-human readable format.
    /// This format is ideal for saving in a compact format.
    /// </summary>
    public class BinaryFormatSerializer : IFormatSerializer
    {
        /// <summary>
        /// Write node data to a stream.
        /// </summary>
        /// <param name="container">Container holding the root node and associated data
        /// to be written to the stream.</param>
        /// <param name="output">Output stream to write data to.</param>
        public void Serialize(Container container, Stream output)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Read node data from a stream.
        /// </summary>
        /// <param name="input">Input stream to read data from.</param>
        /// <returns>Container holding the root node and associated data read from the stream.</returns>
        public Container Deserialize(Stream input)
        {
            throw new NotImplementedException();
        }
    }
}