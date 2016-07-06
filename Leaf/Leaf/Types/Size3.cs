using System;

namespace Leaf.Types
{
    /// <summary>
    /// Storage for a three-dimensional size.
    /// The values are integers, ideal for user interfaces.
    /// This type has an width, height, and depth value.
    /// The depth value can be used for z-layer.
    /// </summary>
    public struct Size3
    {
        /// <summary>
        /// Distance the region spans along the x-axis.
        /// </summary>
        public int Width
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Distance the region spans along the y-axis.
        /// </summary>
        public int Height
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Distance the region spans along the z-axis.
        /// </summary>
        public int Depth
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Creates a new three-dimensional rectangle setting the all of the properties.
        /// </summary>
        /// <param name="width">Distance spanned on the x-axis.</param>
        /// <param name="height">Distance spanned on the y-axis.</param>
        /// <param name="depth">Distance spanned on the z-axis.</param>
        public Size3(int width, int height, int depth)
        {
            throw new NotImplementedException();
        }
    }
}
