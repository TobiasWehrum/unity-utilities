using System.Collections.Generic;
using UnityEngine;

namespace UnityUtilities
{
    /// <summary>
    /// Stores the vertex/triangle data of a mesh in easily modifiable form - which can
    /// then be used to actually create or update a mesh.
    /// </summary>
    public class MeshCreator
    {
        /// <summary>
        /// The triangles of the mesh.
        /// </summary>
        List<MeshTriangle> meshTriangles;

        /// <summary>
        /// The vertices of a mesh.
        /// </summary>
        List<MeshVertex> meshVertices;

        /// <summary>
        /// A temporary array of the vertex positions for filling the mesh.
        /// </summary>
        Vector3[] tempVertices;

        /// <summary>
        /// A temporary array of the vertex normals for filling the mesh.
        /// </summary>
        Vector3[] tempNormals;

        /// <summary>
        /// A temporary array of the vertex UV coordinates for filling the mesh.
        /// </summary>
        Vector2[] tempUV;

        /// <summary>
        /// A temporary array of the vertex colors for filling the mesh.
        /// </summary>
        Color[] tempColors;

        /// <summary>
        /// A temporary array of the triangle vertex indizes for filling the mesh.
        /// </summary>
        int[] tempTriangles;

        /// <summary>
        /// The amount of registered vertices.
        /// </summary>
        public int MeshVerticesCount
        {
            get { return meshVertices.Count; }
        }

        /// <summary>
        /// The registered vertices. Use <see cref="AddVertex"/> to add a vertex.
        /// Content of existing vertices can be freely updated.
        /// </summary>
        public List<MeshVertex> MeshVertices
        {
            get { return meshVertices; }
        }

        /// <summary>
        /// The registered triangles. Use <see cref="AddTriangle"/> or <see cref="AddQuad"/>
        /// to add triangles.
        /// </summary>
        public List<MeshTriangle> MeshTriangles
        {
            get { return meshTriangles; }
        }

        /// <summary>
        /// Creates a new MeshCreator.
        /// </summary>
        /// <param name="estimatedTriangleCount">Used to create the initial size of the MeshTriangles list.</param>
        /// <param name="estimatedVertexCount">Used to set the initial size of the MeshVertices list.</param>
        public MeshCreator(int estimatedTriangleCount, int estimatedVertexCount)
        {
            meshTriangles = new List<MeshTriangle>(estimatedTriangleCount);
            meshVertices = new List<MeshVertex>(estimatedVertexCount);
        }

        /// <summary>
        /// Clear the triangles and vertices.
        /// </summary>
        public void Clear()
        {
            meshTriangles.Clear();

            for (var i = 0; i < meshVertices.Count; i++)
                meshVertices[i].VertexIndex = -1;

            meshVertices.Clear();
        }

        /// <summary>
        /// Adds a vertex to the MeshCreator and assigns it a VertexIndex.
        /// If it already has a VertexIndex, this method does nothing.
        /// </summary>
        /// <param name="vertex">The vertex to be added.</param>
        public MeshVertex AddVertex(MeshVertex vertex)
        {
            if (vertex.VertexIndex != -1)
                return vertex;

            vertex.VertexIndex = meshVertices.Count;
            meshVertices.Add(vertex);
            return vertex;
        }

        /// <summary>
        /// Adds a triangle to the MeshCreator. Adds the vertices to the MeshCreator
        /// if they aren't already added.
        /// </summary>
        /// <param name="a">The first vertex in clockwise order.</param>
        /// <param name="b">The second vertex in clockwise order.</param>
        /// <param name="c">The third vertex in clockwise order.</param>
        /// <returns>The created triangle.</returns>
        public MeshTriangle AddTriangle(MeshVertex a, MeshVertex b, MeshVertex c)
        {
            AddVertex(a);
            AddVertex(b);
            AddVertex(c);

            var triangle = new MeshTriangle(a, b, c);
            meshTriangles.Add(triangle);
            return triangle;
        }

        /// <summary>
        /// Adds two triangles forming a quad to the MeshCreator. Adds the vertices
        /// to the MeshCreator if they aren't already added.
        /// </summary>
        /// <param name="a">The first vertex in clockwise order.</param>
        /// <param name="b">The second vertex in clockwise order.</param>
        /// <param name="c">The third vertex in clockwise order.</param>
        /// <param name="d">The fourth vertex in clockwise order.</param>
        public void AddQuad(MeshVertex a, MeshVertex b, MeshVertex c, MeshVertex d)
        {
            AddTriangle(a, b, c);
            AddTriangle(a, c, d);
        }

        /// <summary>
        /// Creates a new mesh and fills it via <see cref="UpdateMesh"/>.
        /// </summary>
        /// <returns>The filled mesh.</returns>
        public Mesh CreateMesh()
        {
            var mesh = new Mesh();
            mesh.MarkDynamic();
            return UpdateMesh(mesh);
        }

        /// <summary>
        /// Takes an existing mesh previously created with <see cref="CreateMesh"/> or <see cref="UpdateMesh"/>
        /// and fills it with the data from this MeshCreator.
        /// </summary>
        /// <param name="mesh">The mesh to be updated.</param>
        /// <param name="updatePositions">Should the vertex positions be updated?</param>
        /// <param name="updateNormals">Should the vertex normals be updated?</param>
        /// <param name="updateColors">Should the vertex colors be updated?</param>
        /// <param name="updateUVs">Should the UVs be updated?</param>
        /// <param name="updateTriangles">
        /// Should the triangle composition be updated? This only needs to be called when
        /// you assigned new vertices to triangles, not when vertices changed their data
        /// internally. Calling this also triggers updating every other property.
        /// </param>
        /// <returns>The filled mesh.</returns>
        public Mesh UpdateMesh(Mesh mesh, bool updatePositions = true, bool updateNormals = true, bool updateColors = true, bool updateUVs = true, bool updateTriangles = true)
        {
            if (updateTriangles)
            {
                mesh.Clear();
                updatePositions = true;
                updateNormals = true;
                updateColors = true;
                updateUVs = true;
            }

            var verticesCount = meshVertices.Count;
            if ((tempVertices == null) || (tempVertices.Length != verticesCount))
            {
                tempVertices = new Vector3[verticesCount];
                tempNormals = new Vector3[verticesCount];
                tempUV = new Vector2[verticesCount];
                tempColors = new Color[verticesCount];
            }

            for (var i = 0; i < verticesCount; i++)
            {
                var meshVertex = meshVertices[i];

                if (updatePositions)
                    tempVertices[i] = meshVertex.Position;

                if (updateNormals)
                    tempNormals[i] = meshVertex.Normal;

                if (updateColors)
                    tempColors[i] = meshVertex.Color;

                if (updateUVs)
                    tempUV[i] = meshVertex.UV;
            }

            if (updatePositions)
                mesh.vertices = tempVertices;

            if (updateColors)
                mesh.colors = tempColors;

            if (updateNormals)
                mesh.normals = tempNormals;

            if (updateUVs)
                mesh.uv = tempUV;

            if (updateTriangles)
            {
                var trianglesCount = meshTriangles.Count;
                var triangleArrayLength = trianglesCount * 3;

                if ((tempTriangles == null) || (tempTriangles.Length != triangleArrayLength))
                    tempTriangles = new int[triangleArrayLength];

                for (var i = 0; i < trianglesCount; i++)
                {
                    var meshTriangleVertices = meshTriangles[i].MeshVertices;
                    for (var j = 0; j < 3; j++)
                    {
                        tempTriangles[i * 3 + j] = meshTriangleVertices[j].VertexIndex;
                    }
                }

                mesh.triangles = tempTriangles;
            }

            if (updatePositions)
                mesh.RecalculateBounds();

            return mesh;
        }
    }
}