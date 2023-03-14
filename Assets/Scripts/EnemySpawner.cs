using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	[SerializeField] private List<GameObject> enemyPrefabs;

	[SerializeField] private float spawnInterval = 5.0f;
	private float spawnCounter = 0.0f;

	[SerializeField] private EnemySpawnPoint[] enemySpawnPoints;

	//[SerializeField] private List<GameObject> spawnedEnemies;

	private void Start() {
		enemySpawnPoints = FindObjectsOfType<EnemySpawnPoint>();

	}

	private void Update() {
		spawnCounter += Time.deltaTime;

		if ( spawnCounter > spawnInterval ) {
			foreach ( EnemySpawnPoint enemySpawnPoint in enemySpawnPoints ) {
				enemySpawnPoint.SpawnHere( enemyPrefabs[ Random.Range( 0, enemyPrefabs.Count ) ] );
			}

			//spawnCounter = 0.0f;
			spawnCounter = -1000000.0f;
		}
	}

}
