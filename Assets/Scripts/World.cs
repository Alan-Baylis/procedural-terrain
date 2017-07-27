using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
	public WorldSetting worldSettings;
	public Material material;

	[HideInInspector]
	public bool autoUpdate;

	List<Chunk> visibleChunks = new List<Chunk> ();
	Dictionary<Vector2, Chunk> chunks = new Dictionary<Vector2, Chunk> ();

	public MeshFilter meshFilter;

	public float speed = 10f;

	private void Start() {
		StartCoroutine (UpdateChunks ());
	}

	IEnumerator UpdateChunks() {
		while (true) {
			float heightMultiplayer = worldSettings.heightMultiplayer;
			material.SetFloat ("minHeight", -heightMultiplayer);
			material.SetFloat ("maxHeight", heightMultiplayer);

			yield return Generate ();

			//for (int z = -2; z <= 2; z++) {
			//	for (int x = -2; x <= 2; x++) {
			//		Vector2 coord = new Vector2 (x, z);

			//		Chunk chunk;
			//		if (chunks.TryGetValue(coord, out chunk)) {
			//			chunk.Update ();
			//		} else {
			//
			//		}
			//	}
			//}
		}
	}

	private void FixedUpdate() {
		worldSettings.offset.y += Time.deltaTime * speed;
	}

	public bool Generate() {
		int chunkSize = worldSettings.chunkSize;
		int meshSize = chunkSize + 1;

		int halfSize = meshSize / 2;

		var meshBuilder = new MeshBuilder ();

		var offset = worldSettings.offset;
		float xOffset = offset.x;
		float zOffset = offset.y;

		float scale = worldSettings.scale;
		float persistence = worldSettings.persistence;
		float lacunarity = worldSettings.lacunarity;

		for (int z = 0, i = 0; z < meshSize; z++) {
			for (int x = 0; x < meshSize; x++, i++) {
				int octaves = worldSettings.octaves;

				float amplitude = 1f;
				float frequency = 1f;

				float y = 0f;

				while (octaves-- > 0) {
					float sx = (x + xOffset) / scale * frequency;
					float sz = (z + zOffset) / scale * frequency;

					float noise = Mathf.PerlinNoise (sx, sz) * 2f - 1f;
					y += noise * amplitude;

					amplitude *= persistence;
					frequency *= lacunarity;
				}

				meshBuilder.AddVertex (x - halfSize, y * worldSettings.heightMultiplayer, z - halfSize);
				meshBuilder.AddUV (x / (float) meshSize, z / (float) meshSize);
				if (z < chunkSize && x < chunkSize) {
					meshBuilder.AddTriangle (i + meshSize, i + 1, i);
					meshBuilder.AddTriangle (i + meshSize + 1, i + 1, i + meshSize);
				}
			}
		}

		meshFilter.mesh = meshBuilder.Create ();

		return false;
	}
}

[System.Serializable]
public class WorldSetting {
	public int seed;

	public int chunkSize;
	public int octaves;
	public float scale;
	public float persistence;
	public float lacunarity;
	public float heightMultiplayer;
	public Vector2 offset;
}