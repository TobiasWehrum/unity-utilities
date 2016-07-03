namespace UnityUtilities
{
    /// <summary>
    /// A triangle, consisting of three <see cref="MeshVertex"/>.
    /// Used in a <see cref="MeshCreator"/>. Cannot be part of more than one MeshCreator.
    /// </summary>
    public class MeshTriangle
    {
        /// <summary>
        /// The three vertices that make up this triangle in clockwise order.
        /// </summary>
        public MeshVertex[] MeshVertices { get; private set; }

        /// <summary>
        /// Creates a new triangle.
        /// </summary>
        /// <param name="a">The first vertex in clockwise order.</param>
        /// <param name="b">The second vertex in clockwise order.</param>
        /// <param name="c">The third vertex in clockwise order.</param>
        public MeshTriangle(MeshVertex a, MeshVertex b, MeshVertex c)
        {
            MeshVertices = new[] {a, b, c};
        }

        /// <summary>
        /// Convenience method to update all three vertices at once.
        /// </summary>
        /// <param name="a">The first vertex in clockwise order.</param>
        /// <param name="b">The second vertex in clockwise order.</param>
        /// <param name="c">The third vertex in clockwise order.</param>
        public void Update(MeshVertex a, MeshVertex b, MeshVertex c)
        {
            MeshVertices[0] = a;
            MeshVertices[1] = b;
            MeshVertices[2] = c;
        }
    }
}