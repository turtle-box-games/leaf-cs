namespace Leaf.Types
{
    /// <summary>
    /// Storage for a three-dimensional position and size.
    /// The values are integers, ideal for user interfaces.
    /// This type has an X, Y, Z, width, height, and depth value.
    /// </summary>
    public struct Rect3
    {
        private readonly int _x, _y, _z, _w, _h, _d;

        /// <summary>
        /// Offset along the x-axis of the region.
        /// </summary>
        public int X
        {
            get { return _x; }
        }

        /// <summary>
        /// Offset along the y-axis of the region.
        /// </summary>
        public int Y
        {
            get { return _y; }
        }

        /// <summary>
        /// Offset along the z-axis of the region.
        /// </summary>
        public int Z
        {
            get { return _z; }
        }

        /// <summary>
        /// Distance the region spans along the x-axis.
        /// </summary>
        public int Width
        {
            get { return _w; }
        }

        /// <summary>
        /// Distance the region spans along the y-axis.
        /// </summary>
        public int Height
        {
            get { return _h; }
        }

        /// <summary>
        /// Distance the region spans along the z-axis.
        /// </summary>
        public int Depth
        {
            get { return _d; }
        }

        /// <summary>
        /// Creates a new three-dimensional rectangle setting all properties.
        /// </summary>
        /// <param name="x">Offset along the x-axis.</param>
        /// <param name="y">Offset along the y-axis.</param>
        /// <param name="z">Offset along the z-axis.</param>
        /// <param name="width">Distance spanned on the x-axis.</param>
        /// <param name="height">Distance spanned on the y-axis.</param>
        /// <param name="depth">Distance spanned on the z-axis.</param>
        public Rect3(int x, int y, int z, int width, int height, int depth)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = width;
            _h = height;
            _d = depth;
        }

        /// <summary>
        /// Creates a new three-dimensional rectangle setting the size properties manually and the position to (0, 0, 0).
        /// </summary>
        /// <param name="width">Distance spanned on the x-axis.</param>
        /// <param name="height">Distance spanned on the y-axis.</param>
        /// <param name="depth">Distance spanned on the z-axis.</param>
        public Rect3(int width, int height, int depth)
        {
            _x = 0;
            _y = 0;
            _z = 0;
            _w = width;
            _h = height;
            _d = depth;
        }

        /// <summary>
        /// Creates a new three-dimensional rectangle by providing a position and size.
        /// </summary>
        /// <param name="position">Location of the bounds.</param>
        /// <param name="size">Distance the bounds extend.</param>
        public Rect3(Point3 position, Point3 size)
        {
            _x = position.X;
            _y = position.Y;
            _z = position.Z;
            _w = size.X;
            _h = size.Y;
            _d = size.Z;
        }
    }
}
