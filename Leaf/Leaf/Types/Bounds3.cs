namespace Leaf.Types
{
    /// <summary>
    /// Storage for a three-dimensional position and size.
    /// The values are floating-point numbers, ideal for game-play.
    /// This type has an X, Y, Z, width, height, and depth value.
    /// </summary>
    public struct Bounds3
    {
        private readonly float _x, _y, _z, _w, _h, _d;

        /// <summary>
        /// Offset along the x-axis of the bounding container.
        /// </summary>
        public float X
        {
            get { return _x; }
        }

        /// <summary>
        /// Offset along the y-axis of the bounding container.
        /// </summary>
        public float Y
        {
            get { return _y; }
        }

        /// <summary>
        /// Offset along the z-axis of the bounding container.
        /// </summary>
        public float Z
        {
            get { return _z; }
        }

        /// <summary>
        /// Distance the bounding container spans along the x-axis.
        /// </summary>
        public float Width
        {
            get { return _w; }
        }

        /// <summary>
        /// Distance the bounding container spans along the y-axis.
        /// </summary>
        public float Height
        {
            get { return _h; }
        }

        /// <summary>
        /// Distance the bounding container spans along the z-axis.
        /// </summary>
        public float Depth
        {
            get { return _d; }
        }

        /// <summary>
        /// Creates a new three-dimensional bounds setting all properties.
        /// </summary>
        /// <param name="x">Offset along the x-axis.</param>
        /// <param name="y">Offset along the y-axis.</param>
        /// <param name="z">Offset along the z-axis.</param>
        /// <param name="width">Distance spanned on the x-axis.</param>
        /// <param name="height">Distance spanned on the y-axis.</param>
        /// <param name="depth">Distance spanned on the z-axis.</param>
        public Bounds3(float x, float y, float z, float width, float height, float depth)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = width;
            _h = height;
            _d = depth;
        }

        /// <summary>
        /// Creates a new three-dimensional bounds setting the size properties manually and the position to (0, 0, 0).
        /// </summary>
        /// <param name="width">Distance spanned on the x-axis.</param>
        /// <param name="height">Distance spanned on the y-axis.</param>
        /// <param name="depth">Distance spanned on the z-axis.</param>
        public Bounds3(float width, float height, float depth)
        {
            _x = 0f;
            _y = 0f;
            _z = 0f;
            _w = width;
            _h = height;
            _d = depth;
        }

        /// <summary>
        /// Creates a new three-dimensional bounds by providing a position and size.
        /// </summary>
        /// <param name="position">Location of the bounds.</param>
        /// <param name="size">Distance the bounds extend.</param>
        public Bounds3(Vector3 position, Vector3 size)
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
