using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor( typeof( EnemySpawnManager ) )]
public class EnemySpawnManagerEditor : Editor {

	public override void OnInspectorGUI() {

		base.OnInspectorGUI();

		EnemySpawnManager enemySpawnManager = (EnemySpawnManager)target;

		if ( GUILayout.Button( "Create new Enemy Spawner" ) ) {
			enemySpawnManager.CreateNewEnemySpawner();

		}

		enemySpawnManager.RefreshEnemySpawners();

	}

}
