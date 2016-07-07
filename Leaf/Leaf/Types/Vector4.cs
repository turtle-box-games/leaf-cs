namespace Leaf.Types
{
    /// <summary>
    /// Storage for a four-dimensional position, direction, size, or etc.
    /// The values are floating-point numbers.
    /// This type has an X, Y, Z, and W value.
    /// </summary>
    public struct Vector4
    {
        private readonly float _x, _y, _z, _w;

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
        /// Magnitude of the w-axis.
        /// </summary>
        public float W
        {
            get { return _w; }
        }

        /// <summary>
        /// Creates a four-dimensional vector setting all properties.
        /// </summary>
        /// <param name="x">Magnitude of the x-axis.</param>
        /// <param name="y">Magnitude of the y-axis.</param>
        /// <param name="z">Magnitude of the z-axis.</param>
        /// <param name="w">Magnitude of the w-axis.</param>
        public Vector4(float x, float y, float z, float w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        }
    }
}
