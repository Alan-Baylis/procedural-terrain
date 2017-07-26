using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk {
	private GameObject gameObject;
	private Vector2 coord;
	private WorldSetting worldSettings;

	private MeshFilter meshFilter;
	private MeshRenderer meshRenderer;

	public Chunk(Vector2 coord, WorldSetting worldSettings) {
		this.coord = coord;
		this.worldSettings = worldSettings;

		gameObject = new GameObject ("Chunk");
		meshFilter = gameObject.AddComponent<MeshFilter> ();
		meshRenderer = gameObject.AddComponent<MeshRenderer> ();
	}

	public void Generate() {
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
	}

	public void Update() {
		
	}
}