using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnManager : MonoBehaviour {

	[SerializeField] private GameObject enemySpawnerPrefab;

	public List<EnemySpawner> enemySpawners = new List<EnemySpawner>();

	private void Start() {
		enemySpawners = new List<EnemySpawner>();

	}

	public void CreateNewEnemySpawner() {
		GameObject newEnemySpawnerObject = Instantiate( enemySpawnerPrefab, transform );

		newEnemySpawnerObject.transform.parent = transform;

		EnemySpawner newEnemySpawner = newEnemySpawnerObject.GetComponent<EnemySpawner>();

		newEnemySpawner.enemySpawnManager = this;

		enemySpawners.Add( newEnemySpawner );

		RefreshEnemySpawners();

	}

	public void RefreshEnemySpawners() {
		for ( int i = 0; i < enemySpawners.Count; i++ ) {
			enemySpawners[i].spawnerId = i;

		}
	}

}
