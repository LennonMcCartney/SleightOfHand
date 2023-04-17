using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif //UNITY_EDITOR

public class EnemySpawnManager : MonoBehaviour {

	[SerializeField] private GameObject enemySpawnerPrefab;
	[SerializeField] private GameObject spawnPointPrefab;

	[SerializeField] private float testVar;

	[SerializeField] private List<EnemySpawner> enemySpawners;

	private void Start() {
		//enemySpawners = new List<EnemySpawner>();

	}

	private void Update() {
		Debug.Log( enemySpawners.Count );
	}

	public void CreateNewEnemySpawner() {
		GameObject newEnemySpawnerObject = Instantiate( enemySpawnerPrefab, transform );

		newEnemySpawnerObject.transform.parent = transform;

		EnemySpawner newEnemySpawner = newEnemySpawnerObject.GetComponent<EnemySpawner>();

		newEnemySpawner.enemySpawnManager = this;
		newEnemySpawner.spawnPointPrefab = spawnPointPrefab;

		Undo.RecordObject( newEnemySpawner, "Something" );

		enemySpawners.Add( newEnemySpawner );

		testVar = 3.0f;

	}

}
