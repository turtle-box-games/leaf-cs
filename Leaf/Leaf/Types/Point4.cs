using System;

namespace Leaf.Types
{
    /// <summary>
    /// Storage for a four-dimensional point or size.
    /// The values are integers.
    /// This type has an X, Y, Z, and W value.
    /// </summary>
    public struct Point4
    {
        private readonly int _x, _y, _z, _w;

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
        /// Offset along the w-axis.
        /// </summary>
        public int W
        {
            get { return _w; }
        }

        /// <summary>
        /// Creates a four-dimensional point setting all properties.
        /// </summary>
        /// <param name="x">Offset along the x-axis.</param>
        /// <param name="y">Offset along the y-axis.</param>
        /// <param name="z">Offset along the z-axis.</param>
        /// <param name="w">Offset along the w-axis.</param>
        public Point4(int x, int y, int z, int w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        }
    }
}
