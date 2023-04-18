using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor( typeof( EnemySpawner ) )]
public class EnemySpawnerEditor : Editor {

	public override void OnInspectorGUI() {

		DrawDefaultInspector();

		if ( GUILayout.Button( "Create new Enemy Spawn Point" ) ) {
			EnemySpawner enemySpawner = (EnemySpawner)target;
			//Undo.RecordObject( enemySpawner, "Create new Enemy Spawn Point" );
			enemySpawner.CreateSpawnPoint();

		}

	}

}
