namespace Pbrt.Core
{
    public class Shape
    {
        /// <summary>
        /// False by default, meaning for closed shapes, normals point to the outside of the shape. 
        /// If true, normals are flipped and point to the inside of the shape.
        /// </summary>
        public bool ReverseOrientation { get; internal set; }

        /// <summary>
        /// True if the Shape’s transformation matrix is switching the handedness of the object coordinate system
        /// (like a scale of (1, 1, -1) for example).
        /// </summary>
        public bool TransformSwapsHandedness { get; internal set; }
    }
}