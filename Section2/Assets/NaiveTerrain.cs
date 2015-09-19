using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class NaiveTerrain : MonoBehaviour {
	public int m_size = 4;
	public float m_cellSize = 1.0f;

	// Use this for initialization
	void Start () {
		Regenerate ();
	}
	
	private Mesh MakeMesh ()
	{
		Mesh result = new Mesh ();
		result.vertices = GenVertices ();
		result.triangles = GenTriangles ();
		result.uv = GenUVs();
		result.RecalculateNormals ();
		return result;
	}

	Vector3[] GenVertices ()
	{
		int w = m_size + 1;
		int l = m_size + 1;
		Vector3[] result = new Vector3[w * l];
		for(int x = 0; x < w; x++) {
			for(int z = 0; z < l; z++) {
				int i = x * l + z;
				result[i] = new Vector3(x * m_cellSize, Random.value, z * m_cellSize);
			}
		}
		return result;
	}

	public void Regenerate ()
	{
		MeshFilter mf = GetComponent<MeshFilter> ();	
		mf.mesh = MakeMesh ();
	}

	int[] GenTriangles ()
	{
		int numCells = m_size * m_size;
		int numTrianglesPerCell = 2;
		int numVerticesPerTriangle = 3;
		int[] triangles = new int[numCells * numTrianglesPerCell * numVerticesPerTriangle];
		int tindex = 0;
		for(int cx = 0; cx < m_size; cx++) {
			for(int cz = 0; cz < m_size; cz++) {
				int n = cx * (m_size + 1) + cz;

				triangles[tindex++] = n;
				triangles[tindex++] = n+1;
				triangles[tindex++] = n+m_size+2;
				triangles[tindex++] = n;
				triangles[tindex++] = n+m_size + 2;
				triangles[tindex++] = n+m_size+1;
			}
		}
		return triangles;
	}

	Vector2[] GenUVs ()
	{
		int w = m_size + 1;
		int l = m_size + 1;
		Vector2[] uvs = new Vector2[w * l];

		for(int ux = 0; ux < w; ux++) {
			for(int uz = 0; uz < w; uz++) {
				uvs[ux*l + uz] = new Vector2(ux/(float)m_size, uz/(float)m_size);
			}
		}
		return uvs;
	}
}
