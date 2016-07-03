using UnityEngine;

namespace UnityUtilities
{
    /// <summary>
    /// A vertex in a <see cref="MeshTriangle"/>. Can -and should!- be shared by multiple triangles.
    /// Used in a <see cref="MeshCreator"/>. Cannot be part of more than one MeshCreator.
    /// </summary>
    public class MeshVertex
    {
        /// <summary>
        /// The local position of the vertex inside the mesh.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// The normal of the vertex.
        /// </summary>
        public Vector3 Normal { get; set; }

        /// <summary>
        /// The color of the vertex.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// The first UV coordinates of the vertex.
        /// </summary>
        public Vector2 UV { get; set; }

        /// <summary>
        /// The index of this vertex inside the MeshCreator.MeshVertices list. Automatically assigned
        /// by MeshCreator.AddVertex(), MeshCreator.AddTriangle() and MeshCreator.AddQuad().
        /// Used to identify the vertex for actually creating the Mesh in MeshCreator.UpdateMesh().
        /// </summary>
        public int VertexIndex { get; internal set; }

        /// <summary>
        /// Creates a new MeshVertex.
        /// </summary>
        /// <param name="position">The local position of the vertex inside the mesh.</param>
        /// <param name="normal">The normal of the vertex.</param>
        /// <param name="color">The color of the vertex.</param>
        /// <param name="uv">The first UV coordinates of the vertex.</param>
        public MeshVertex(Vector3 position, Vector3 normal = default(Vector3), Color color = new Color(), Vector2 uv = new Vector2())
        {
            Position = position;
            Normal = normal;
            Color = color;
            UV = uv;
            VertexIndex = -1;
        }
    }
}