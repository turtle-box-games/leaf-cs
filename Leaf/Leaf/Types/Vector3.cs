namespace Leaf.Types
{
    /// <summary>
    /// Storage for a three-dimensional position, direction, size, or etc.
    /// The values are floating-point numbers.
    /// This type has an X, Y, and Z value.
    /// </summary>
    public struct Vector3
    {
        private readonly float _x, _y, _z;

        /// <summary>
        /// Magnitude of the x-axis.
        /// </summary>
        public float X
        {
            get { return _x; }
        }

        /// <summary>
        /// Magnitude of the y-axis.
        /// </summary>
        public float Y
        {
            get { return _y; }
        }

        /// <summary>
        /// Magnitude of the z-axis.
        /// </summary>
        public float Z
        {
            get { return _z; }
        }

        /// <summary>
        /// Creates a three-dimensional vector setting all properties.
        /// </summary>
        /// <param name="x">Magnitude of the x-axis.</param>
        /// <param name="y">Magnitude of the y-axis.</param>
        /// <param name="z">Magnitude of the z-axis.</param>
        public Vector3(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
    }
}
