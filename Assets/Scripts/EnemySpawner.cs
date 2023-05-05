using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif //UNITY_EDITOR

public class EnemySpawner : MonoBehaviour {

	private bool reachedByPlayer = false;

	private Player player;

	[SerializeField] private int numOfEnemiesToSpawn;

	//[SerializeField] private int numOfEnemiesShielded;

	[SerializeField] [Range(1,100)] private int probabilityShielded;

	private List<int> enemiesShielded;

	private int numOfEnemiesSpawned = 0;
	private int numOfEnemiesKilled = 0;

	private bool killedAllEnemies = false;

	[SerializeField] private float spawnInterval = 5.0f;
	private float spawnCounter = 0.0f;

	[SerializeField] private float enemySpeed;

	[SerializeField] private float triangleEnemyFlightTime;

	[SerializeField] private SpawnPoint[] spawnPoints;

	[SerializeField] private List<GameObject> enemyPrefabs;

	[HideInInspector] public GameObject spawnPointPrefab;

	[HideInInspector] public EnemySpawnManager enemySpawnManager;

	private List<GameObject> spawnedEnemies = new List<GameObject>();

	private void Start() {

		player = FindObjectOfType<Player>();

		RefreshSpawnPoints();

		spawnCounter = spawnInterval;

		//Debug.Log( "numOfEnemiesShielded > " + numOfEnemiesShielded );
		//for ( int i = 0; i < numOfEnemiesShielded; i++ ) {
		//	Debug.Log( "i > " + i );
		//	int enemyShielded = Random.Range( 0, numOfEnemiesToSpawn );
		//	//Debug.Log( "enemyShielded > " + enemyShielded );
		//	while ( enemiesShielded.Contains( enemyShielded ) ) {
		//		enemyShielded = Random.Range( 0, numOfEnemiesToSpawn );
		//		Debug.Log( "enemyShielded > " + enemyShielded );
		//	}

		//	//enemiesShielded.Add( enemyShielded );

		//	//Debug.Log( "enemiesShielded > " + enemiesShielded );

		//}

	}

	private void Update() {
		if ( reachedByPlayer && numOfEnemiesSpawned < numOfEnemiesToSpawn ) {
			spawnCounter += Time.deltaTime;
			if ( spawnCounter >= spawnInterval ) {
				//Debug.Log( spawnedEnemies.Count );

				SpawnPoint spawnPoint = spawnPoints[ Random.Range( 0, spawnPoints.Length ) ];

				//foreach ( SpawnPoint spawnPoint in spawnPoints ) {
				GameObject newEnemyObject = Instantiate( enemyPrefabs[ Random.Range( 0, enemyPrefabs.Count ) ], spawnPoint.transform );
				//newEnemyObject.transform.SetParent( null );
				Enemy newEnemy = newEnemyObject.GetComponent<Enemy>();
				newEnemy.Spawner = this;
				newEnemy.Speed = enemySpeed;
				newEnemy.transform.parent = transform.parent;

				if ( newEnemy.TryGetComponent( out TriangleEnemy triangleEnemy ) ) {
					triangleEnemy.FlightTime = triangleEnemyFlightTime;
				}

				numOfEnemiesSpawned++;

				float probabilityCheck = Random.Range( 1, 101 );

				if ( probabilityCheck <= probabilityShielded ) {
					//Debug.Log( "HasShield" );
					newEnemy.HasShield();
				}

				//if ( enemiesShielded.Contains( numOfEnemiesSpawned ) ) {
				//newEnemy.HasShield();
				//}

				//Debug.Log( "enemiesShielded > " + enemiesShielded );

				spawnedEnemies.Add( newEnemyObject );

				//if ( enemiesShielded.Contains( spawnedEnemies.Count ) ) {
					//newEnemy.HasShield = true;
				//}

				//}

				spawnCounter = 0.0f;
			}
		}

		if ( killedAllEnemies ) {
			player.VirtualCameraController.shouldMove = true;
		}
	}

	private void OnTriggerEnter( Collider other ) {
		if (other.TryGetComponent( out Player collidedPlayer )) {
			reachedByPlayer = true;
			player.VirtualCameraController.shouldMove = false;
		}
	}

	public void RefreshSpawnPoints() {
		spawnPoints = GetComponentsInChildren<SpawnPoint>();

	}

	public void CreateSpawnPoint() {
		GameObject newSpawnPoint = Instantiate( spawnPointPrefab );
#if UNITY_EDITOR
		Undo.RegisterCreatedObjectUndo( newSpawnPoint, "Create new Enemy Spawn Point" );
#endif //UNITY_EDITOR
		newSpawnPoint.transform.parent = transform;

		RefreshSpawnPoints();

	}

	public void DestroyEnemy( Enemy destroyedEnemy ) {

		spawnedEnemies.Remove( destroyedEnemy.gameObject );

		Destroy( destroyedEnemy.gameObject );

		numOfEnemiesKilled++;

		if (numOfEnemiesKilled >= numOfEnemiesToSpawn) {
			killedAllEnemies = true;
		}
	}

}
