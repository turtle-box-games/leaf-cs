using System;

namespace Leaf.Types
{
    /// <summary>
    /// Storage for a three-dimensional position, direction, size, or etc.
    /// The values are floating-point numbers.
    /// This type has an X, Y, and Z value.
    /// </summary>
    public struct Vector3
    {
        /// <summary>
        /// Magnitude of the x-axis.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Magnitude of the y-axis.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Magnitude of the z-axis.
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// Creates a three-dimensional vector setting all properties.
        /// </summary>
        /// <param name="x">Magnitude of the x-axis.</param>
        /// <param name="y">Magnitude of the y-axis.</param>
        /// <param name="z">Magnitude of the z-axis.</param>
        public Vector3(float x, float y, float z)
        {
            throw new NotImplementedException();
        }
    }
}
