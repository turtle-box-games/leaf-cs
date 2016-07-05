namespace Leaf.Types
{
    /// <summary>
    /// Storage for a three-dimensional point or size.
    /// The values are integers, ideal for user interfaces.
    /// This type has an X, Y, and Z value.
    /// </summary>
    public struct Point3
    {
        private readonly int _x, _y, _z;

        /// <summary>
        /// Offset along the x-axis.
        /// </summary>
        public int X
        {
            get { return _x; }
        }

        /// <summary>
        /// Offset along the y-axis.
        /// </summary>
        public int Y
        {
            get { return _y; }
        }

        /// <summary>
        /// Offset along the z-axis.
        /// </summary>
        public int Z
        {
            get { return _z; }
        }

        /// <summary>
        /// Creates a three-dimensional point setting all properties.
        /// </summary>
        /// <param name="x">Offset along the x-axis.</param>
        /// <param name="y">Offset along the y-axis.</param>
        /// <param name="z">Offset along the z-axis.</param>
        public Point3(int x, int y, int z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
    }
}
