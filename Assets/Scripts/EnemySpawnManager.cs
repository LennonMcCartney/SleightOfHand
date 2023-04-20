using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif //UNITY_EDITOR

[ExecuteInEditMode]
public class EnemySpawnManager : MonoBehaviour {

	[SerializeField] private GameObject enemySpawnerPrefab;
	[SerializeField] private GameObject spawnPointPrefab;

	[SerializeField] private EnemySpawner[] enemySpawners;

	private void Start() {
		Undo.undoRedoPerformed += RefreshEnemySpawners;

		//RefreshEnemySpawners();

	}

	public void CreateNewEnemySpawner() {
		GameObject newEnemySpawnerObject = Instantiate( enemySpawnerPrefab );

#if UNITY_EDITOR
		Undo.RegisterCreatedObjectUndo( newEnemySpawnerObject, "Create new Enemy Spawner" );
		//Undo.RecordObject( newEnemySpawnerObject, "Create new Enemy Spawner" );

#endif //UNITY_EDITOR

		//newEnemySpawnerObject.transform.parent = transform;

		EnemySpawner newEnemySpawner = newEnemySpawnerObject.GetComponent<EnemySpawner>();

		newEnemySpawner.enemySpawnManager = this;
		newEnemySpawner.spawnPointPrefab = spawnPointPrefab;

		RefreshEnemySpawners();

	}

	private void RefreshEnemySpawners() {
		Debug.Log( "RefreshEnemySpawners" );
		enemySpawners = GetComponentsInChildren<EnemySpawner>();

		//foreach ( EnemySpawner enemySpawner in enemySpawners ) {
			//Undo.undoRedoPerformed += enemySpawner.RefreshSpawnPoints;
		//}

	}

}
