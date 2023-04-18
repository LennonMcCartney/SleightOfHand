using UnityEngine;
using UnityEditor;

[CustomEditor( typeof( EnemySpawnManager ) )]
public class EnemySpawnManagerEditor : Editor {

	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		if ( GUILayout.Button( "Create new Enemy Spawner" ) ) {
			EnemySpawnManager enemySpawnManager = (EnemySpawnManager)target;
			enemySpawnManager.CreateNewEnemySpawner();

		}

	}

}
