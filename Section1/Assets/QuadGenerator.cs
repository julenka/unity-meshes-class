using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class QuadGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {


		MeshFilter mf = GetComponent<MeshFilter> ();
		mf.mesh = MakeQuad(new Vector3(0,1,0), new Vector3(1,0,0));
	}

	public static Vector3[] CalculateNormals (Mesh mesh)
	{
		Vector3[] vertices = mesh.vertices;
		int[] triangles = mesh.triangles;
		Vector3[] normals = new Vector3[vertices.Length];
		for (int i = 0; i < normals.Length; i++) {
			normals [i] = Vector3.zero;
		}
		for (int i = 0; i < triangles.Length - 2; i += 3) {
			Vector3 tA = vertices [triangles [i]];
			Vector3 tB = vertices [triangles [i + 1]];
			Vector3 tC = vertices [triangles [i + 2]];
			Vector3 normal = Vector3.Cross (tB - tA, tC - tA);
			normals [triangles [i]] += normal;
			normals [triangles [i + 1]] += normal;
			normals [triangles [i + 2]] += normal;
		}
		for (int i = 0; i < normals.Length; i++) {
			normals [i] = normals [i].normalized;
		}
		return normals;
	}

	public static Mesh MakeQuad(Vector3 a, Vector3 b) {
		Vector3[] vertices = new Vector3[4];
		vertices [0] = Vector3.zero;
		vertices [1] = a;
		vertices [2] = a + b;
		vertices [3] = b;
		
		int[] triangles = new int[] {0, 1, 2, 0, 2, 3};
		
		// right side up
		Vector2[] uvs = new Vector2[] {
			new Vector2(0, 0),
			new Vector2(0, 1),
			new Vector2(1, 1),
			new Vector2(1, 0)
		};
		
		
		Mesh mesh = new Mesh ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs;
		mesh.RecalculateNormals ();
		return mesh;
	}
}
