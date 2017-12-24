namespace Leaf.Serialization
{
    /// <summary>
    /// Responsible for serializing and deserializing node data to a persistent format.
    /// </summary>
    /// <typeparam name="TFormat">Serialized format type.</typeparam>
    public interface IFormatSerializer<TFormat>
    {
        /// <summary>
        /// Generate output containing the nodes in a container.
        /// </summary>
        /// <param name="container">Container holding the root node and associated data to be serialized.</param>
        /// <returns>Output containing the serialized node data.</returns>
        TFormat Serialize(Container container);

        /// <summary>
        /// Create a container from serialized node data.
        /// </summary>
        /// <param name="input">Serialized node data to create a container from.</param>
        /// <returns>Container holding the root node and associated data pull from the input.</returns>
        Container Deserialize(TFormat input);
    }
}