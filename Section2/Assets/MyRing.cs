using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class MyRing : MonoBehaviour {

	// Params for the ring
	public int m_numVerticesPerCircle = 36;
	public float m_outerRadius = 5;
	public float m_innerRadius = 2;

	void Start() {
		Mesh mesh = MakeMyRing ();
			
		MeshFilter mf = GetComponent<MeshFilter> ();	
		mf.mesh = mesh;
	}

	/// <summary>
	/// Makes a donut that faces you
	/// </summary>
	/// <returns>The my mesh.</returns>
	private Mesh MakeMyRing() {
		Mesh result = new Mesh ();
		int numVerticesPerCircle = m_numVerticesPerCircle;
		float outerRadius = m_outerRadius;
		float innerRadius = m_innerRadius;
		// Make the vertices
		Vector3[] vertices = new Vector3[numVerticesPerCircle * 2];
		for (int i = 0; i < vertices.Length; i+=2) {
			int thetaIndex = i / 2;
			float theta = Mathf.PI * 2 / numVerticesPerCircle * thetaIndex;

			// Add outer circle first
			float y = Mathf.Sin(theta) * outerRadius;
			float x = Mathf.Cos(theta) * outerRadius;
			float z = 0;
			vertices[i] = new Vector3(x, y, z);

			x = Mathf.Cos(theta) * innerRadius;
			y = Mathf.Sin(theta) * innerRadius;
			z = 0;
			vertices[i + 1] = new Vector3(x, y, z);
		}
		result.vertices = vertices;

		// Make the triangles
		int numTriangles = vertices.Length;
		int[] triangles = new int[numTriangles * 3];
		int tindex = 0;
		for(int triangleIndex = 0; triangleIndex < numTriangles - 1; triangleIndex += 2) {
			int modValue= numTriangles;
			triangles[tindex++] = triangleIndex % modValue;
			triangles[tindex++] = (triangleIndex + 1) % modValue;
			triangles[tindex++] = (triangleIndex + 2) % modValue;
			triangles[tindex++]= (triangleIndex + 1) % modValue;
			triangles[tindex++]= (triangleIndex + 3) % modValue;
			triangles[tindex++]= (triangleIndex + 2) % modValue;
		}
		result.triangles = triangles;
		result.RecalculateBounds ();
		result.RecalculateNormals ();
		return result;
	}
}
