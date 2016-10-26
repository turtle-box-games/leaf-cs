﻿using System;
using System.IO;

namespace Leaf.Nodes
{
    /// <summary>
    /// Storage for a date and time.
    /// Smallest precision (unit) available is 1 microsecond.
    /// Available date and time range is January 1, 1900 through December 31, 9999.
    /// </summary>
    public class TimeNode : Node
    {
        /// <summary>
        /// Retrieve the ID for the type of node.
        /// This can be used to identify, serialize, and cast a node to its type.
        /// The value returned by this property is <see cref="NodeId.Time"/>.
        /// </summary>
        public override NodeId TypeId => NodeId.Time;

        /// <summary>
        /// Gets and sets the value of the node.
        /// </summary>
        public DateTime Value { get; set; }

        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="value">Value of the node.</param>
        public TimeNode(DateTime value)
        {
            Value = value;
        }

        /// <summary>
        /// Creates a new node by reading its contents from a stream.
        /// </summary>
        /// <param name="reader">Reader used to pull data from the stream.</param>
        /// <returns>Newly constructed node.</returns>
        internal TimeNode Read(BinaryReader reader)
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
