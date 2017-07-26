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

	private void Start() {
		StartCoroutine (UpdateChunks ());
	}

	IEnumerator UpdateChunks() {
		while (true) {
			float heightMultiplayer = worldSettings.heightMultiplayer;
			material.SetFloat ("minHeight", -heightMultiplayer);
			material.SetFloat ("maxHeight", heightMultiplayer);

			for (int z = -2; z <= 2; z++) {
				for (int x = -2; x <= 2; x++) {
					Vector2 coord = new Vector2 (x, z);

					Chunk chunk;
					if (chunks.TryGetValue(coord, out chunk)) {
						chunk.Update ();
					} else {

					}
				}
			}
		}
	}

	public void Generate() {
		
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