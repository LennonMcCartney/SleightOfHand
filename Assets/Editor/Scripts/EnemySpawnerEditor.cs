using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor( typeof( EnemySpawner ) )]
public class EnemySpawnerEditor : Editor {

	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		EnemySpawner enemySpawner = (EnemySpawner)target;

		if ( GUILayout.Button( "Create new Enemy Spawn Point" ) ) {
			enemySpawner.CreateSpawnPoint();

		}

	}

}