using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class CubeGenerator : MonoBehaviour {

	void Start () {
		List<Vector2> uvs = new List<Vector2> ();

		Mesh front = QuadGenerator.MakeQuad (new Vector3(0, 1, 0), new Vector3 (1, 0, 0));
		uvs.Add (new Vector2(0, 1 / 3f));
		uvs.Add (new Vector2(0f, 2f / 3f));
     	uvs.Add (new Vector2(1f / 4f, 2f / 3f));
     	uvs.Add (new Vector2(1f / 4f, 1f / 3f));

		Mesh bottom = QuadGenerator.MakeQuad (new Vector3 (1, 0, 0), new Vector3 (0, 0, 1));
		uvs.Add (new Vector2(0f, 0f));
		uvs.Add (new Vector2(0, 1f / 3f));
		uvs.Add (new Vector2(1f / 4f, 1f / 3f));
		uvs.Add (new Vector2(1f / 4f, 0));
		
		Mesh top = QuadGenerator.MakeQuad (new Vector3 (0, 0, 1), new Vector3 (1, 0, 0));
		top = MeshBuilder.Offset (top, new Vector3 (0, 1, 0));
		uvs.Add (new Vector2(0, 2f / 3f));
		uvs.Add (new Vector2(0f, 1f));
		uvs.Add (new Vector2(1f / 4f, 1f));
		uvs.Add (new Vector2(1f / 4f, 2f / 3f));


		Mesh left = QuadGenerator.MakeQuad (new Vector3 (0, 0, 1), new Vector3 (0, 1, 0));
		uvs.Add (new Vector2(1f, 1f / 3f));
		uvs.Add (new Vector2(3f / 4f, 1f / 3f));
		uvs.Add (new Vector2(3f / 4f, 2f / 3f));
		uvs.Add (new Vector2(1f, 2f / 3f));

		Mesh back = QuadGenerator.MakeQuad (new Vector3 (1, 0, 0), new Vector3 (0, 1, 0));
		back = MeshBuilder.Offset (back, new Vector3 (0, 0, 1));
		uvs.Add (new Vector2(3f / 4f, 1f / 3f));
		uvs.Add (new Vector2(1f / 2f, 1f / 3f));
		uvs.Add (new Vector2(1f / 2f, 2f / 3f));
		uvs.Add (new Vector2(3f / 4f, 2f / 3f));


		Mesh right = QuadGenerator.MakeQuad (new Vector3 (0, 1, 0), new Vector3 (0, 0, 1));
		right = MeshBuilder.Offset (right, new Vector3 (1, 0, 0));
		uvs.Add (new Vector2(1f / 4f, 1f / 3f));
		uvs.Add (new Vector2(1f / 4f, 2f / 3f));
		uvs.Add (new Vector2(1f / 2f, 2f / 3f));
		uvs.Add (new Vector2(1f / 2f, 1f / 3f));



		Mesh mesh = new Mesh ();
		mesh = MeshBuilder.Combine (mesh, front);
		mesh = MeshBuilder.Combine (mesh, bottom);
		mesh = MeshBuilder.Combine (mesh, top);
		mesh = MeshBuilder.Combine (mesh, left);
		mesh = MeshBuilder.Combine (mesh, back);
		mesh = MeshBuilder.Combine (mesh, right);


		mesh.uv = uvs.ToArray();
		MeshFilter mf = GetComponent<MeshFilter> ();
		mf.mesh = mesh;
	}


}
