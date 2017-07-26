using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBuilder {
	private List<Vector3> vertices;
	private List<int> triangles;
	private List<Vector2> uvs;

	public MeshBuilder() {
		vertices = new List<Vector3> ();
		triangles = new List<int> ();
		uvs = new List<Vector2> ();
	}

	public void AddVertex(float x, float y, float z) {
		vertices.Add (new Vector3 (x, y, z));
	}

	public void AddVertex(Vector3 vertex) {
		vertices.Add (vertex);
	}

	public void AddUV(float x, float y) {
		uvs.Add (new Vector2 (x, y));
	}

	public void AddUV(Vector2 uv) {
		uvs.Add (uv);
	}

	public void AddTriangle(int index0, int index1, int index2) {
		triangles.Add (index0);
		triangles.Add (index1);
		triangles.Add (index2);
	}

	public Mesh Create() {
		var mesh = new Mesh {
			vertices = vertices.ToArray(),
			triangles = triangles.ToArray(),
			uv = uvs.ToArray()
		};

		mesh.RecalculateNormals ();

		return mesh;
	}
}
