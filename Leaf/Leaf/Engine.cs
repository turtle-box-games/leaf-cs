using System;

namespace Leaf
{
    /// <summary>
    /// Handles serialization and meta-data of nodes in a container.
    /// </summary>
    public abstract class Engine
    {
        /// <summary>
        /// Numerical version used to distinguish this engine from others.
        /// </summary>
        public abstract int Version { get; }

        /// <summary>
        /// Header containing information on how the nodes are structured.
        /// </summary>
        protected Header InternalHeader { get; }

        /// <summary>
        /// Create the base of the engine.
        /// </summary>
        /// <param name="header">Header containing information on how the nodes are structured.</param>
        protected Engine(Header header)
        {
            throw new NotImplementedException();
        }
    }
}
