using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(World))]
public class WorldEditor : Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI ();

		World world = (World)target;
		if (!Application.isPlaying) {
			world.autoUpdate = EditorGUILayout.Toggle ("Auto update", world.autoUpdate);

			if (world.autoUpdate) {
				world.Generate ();
			} else if (GUILayout.Button ("Update")) {
				world.Generate ();
			}
		}
	}
}
