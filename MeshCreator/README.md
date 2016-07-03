# MeshCreator

Stores the vertex/triangle data of a mesh in easily modifiable form - which can then be used to actually create or update a mesh.

## Example

```C#
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshCreatorExample : MonoBehaviour
{
	MeshCreator meshCreator;
	MeshFilter meshFilter;

	void Awake()
	{
		// We will create a cube, so we set 12 triangles (6 sides á 2 triangles) and 8 vertices.
		// To do a cube with proper shading, we should actually create 4 vertices per side and have
		// normals pointing outwards, but for this example let's keep it simple.
		meshCreator = new MeshCreator(12, 8);

		// Create the vertices
		var behindUpperLeft = new MeshVertex(new Vector3(-1, 1, 1), Vector3.zero, Color.white);
		var behindUpperRight = new MeshVertex(new Vector3(1, 1, 1), Vector3.zero, Color.white);
		var behindLowerLeft = new MeshVertex(new Vector3(-1, -1, 1), Vector3.zero, Color.white);
		var behindLowerRight = new MeshVertex(new Vector3(1, -1, 1), Vector3.zero, Color.white);
		var frontUpperLeft = new MeshVertex(new Vector3(-1, 1, -1), Vector3.zero, Color.white);
		var frontUpperRight = new MeshVertex(new Vector3(1, 1, -1), Vector3.zero, Color.white);
		var frontLowerLeft = new MeshVertex(new Vector3(-1, -1, -1), Vector3.zero, Color.white);
		var frontLowerRight = new MeshVertex(new Vector3(1, -1, -1), Vector3.zero, Color.white);

		// Create the quads
		meshCreator.AddQuad(frontUpperLeft, frontUpperRight, frontLowerRight, frontLowerLeft);
		meshCreator.AddQuad(behindUpperRight, behindUpperLeft, behindLowerLeft, behindLowerRight);
		meshCreator.AddQuad(frontUpperRight, behindUpperRight, behindLowerRight, frontLowerRight);
		meshCreator.AddQuad(frontUpperLeft, frontLowerLeft, behindLowerLeft, behindUpperLeft);
		meshCreator.AddQuad(frontUpperLeft, behindUpperLeft, behindUpperRight, frontUpperRight);
		meshCreator.AddQuad(frontLowerLeft, frontLowerRight, behindLowerRight, behindLowerLeft);

		// Create a new mesh and set it in the mesh filter
		meshFilter = GetComponent<MeshFilter>();
		meshFilter.sharedMesh = meshCreator.CreateMesh();
		meshFilter.sharedMesh.RecalculateNormals();
	}

	void Update()
	{
		// Make the vertices wobble independently
		for (int i = 0; i < meshCreator.MeshVerticesCount; i++)
		{
			MeshVertex meshVertex = meshCreator.MeshVertices[i];
			float scale = Mathf.Lerp(2f, 4f, Mathf.Abs(Mathf.Cos(Time.time + i / 4f)));
			meshVertex.Position = meshVertex.Position.normalized * scale;
		}

		// Update the mesh
		meshCreator.UpdateMesh(meshFilter.sharedMesh);
	}
}
```

## Dependencies

None.