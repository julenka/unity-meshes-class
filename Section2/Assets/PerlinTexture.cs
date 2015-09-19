using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class PerlinTexture : MonoBehaviour {
	public int pixWidth;
	public int pixHeight;
	public float xOrg;
	public float yOrg;
	public float[] scales = new float[]{1f};
	private Texture2D noiseTex;
	private Color[] pix;
	private Renderer rend;

	void Start() {
		rend = GetComponent<Renderer>();
		noiseTex = new Texture2D(pixWidth, pixHeight);
		pix = new Color[noiseTex.width * noiseTex.height];
		rend.material.mainTexture = noiseTex;
		CalcNoise ();
	}

	void CalcNoise() {
		for (float y = 0; y < noiseTex.height; y++) {
			for(float x = 0; x < noiseTex.width; x++) {
				pix[(int)(y * noiseTex.width + x)] = new Color(0,0,0);
				for (int pi = 0; pi < scales.Length; pi++) {
					float scale = scales[pi];
					float xCoord = xOrg + x / noiseTex.width * scale;
					float yCoord = yOrg + y / noiseTex.height * scale;
					float sample = Mathf.PerlinNoise(xCoord, yCoord);

					sample /= scales.Length;
					
					pix[(int)(y * noiseTex.width + x)].r += sample;
					pix[(int)(y * noiseTex.width + x)].g += sample;
					pix[(int)(y * noiseTex.width + x)].b += sample;
				}


			}
		}

		noiseTex.SetPixels(pix);
		noiseTex.Apply();
	}
}
