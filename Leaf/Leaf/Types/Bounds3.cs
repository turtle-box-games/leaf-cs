using System;

namespace Leaf.Types
{
    /// <summary>
    /// Storage for a three-dimensional position and size.
    /// The values are floating-point numbers, ideal for game-play.
    /// This type has an X, Y, Z, width, height, and depth value.
    /// </summary>
    public struct Bounds3
    {
        /// <summary>
        /// Offset along the x-axis of the bounding container.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Offset along the y-axis of the bounding container.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Offset along the z-axis of the bounding container.
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// Distance the bounding container spans along the x-axis.
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// Distance the bounding container spans along the y-axis.
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// Distance the bounding container spans along the z-axis.
        /// </summary>
        public float Depth { get; set; }

        /// <summary>
        /// Determines whether the size is 0.
        /// </summary>
        public bool IsEmpty
        {
            get { throw new NotImplementedException(); }
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new three-dimensional bounds setting the size properties manually and the position to (0, 0, 0).
        /// </summary>
        /// <param name="width">Distance spanned on the x-axis.</param>
        /// <param name="height">Distance spanned on the y-axis.</param>
        /// <param name="depth">Distance spanned on the z-axis.</param>
        public Bounds3(float width, float height, float depth)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new three-dimensional bounds by providing a position and size.
        /// </summary>
        /// <param name="position">Location of the bounds.</param>
        /// <param name="size">Distance the bounds extend.</param>
        public Bounds3(Point3 position, Vector3 size)
        {
            throw new NotImplementedException();
        }
    }
}
