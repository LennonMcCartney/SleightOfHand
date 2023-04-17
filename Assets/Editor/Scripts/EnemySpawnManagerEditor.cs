using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor( typeof( EnemySpawnManager ) )]
public class EnemySpawnManagerEditor : Editor {

	public override void OnInspectorGUI() {

		//base.OnInspectorGUI();

		if ( GUILayout.Button( "Create new Enemy Spawner" ) ) {
			EnemySpawnManager enemySpawnManager = (EnemySpawnManager)target;
			enemySpawnManager.CreateNewEnemySpawner();

		}

		DrawDefaultInspector();

	}

}
