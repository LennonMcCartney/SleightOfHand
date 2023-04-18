using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif //UNITY_EDITOR

public class EnemySpawner : MonoBehaviour {

	private bool reachedByPlayer = false;

	private Player player;

	[SerializeField] private int numOfEnemies;

	private bool killedAllEnemies = false;

	[SerializeField] private float spawnInterval = 5.0f;
	private float spawnCounter = 0.0f;

	[SerializeField] private float enemySpeed;

	[SerializeField] private SpawnPoint[] spawnPoints;

	[SerializeField] private List<GameObject> enemyPrefabs;

	[HideInInspector]
	public GameObject spawnPointPrefab;

	[HideInInspector]
	public EnemySpawnManager enemySpawnManager;

	private List<GameObject> spawnedEnemies = new List<GameObject>();

	private void Start() {

		player = FindObjectOfType<Player>();

		RefreshSpawnPoints();

	}

	private void Update() {
		if ( reachedByPlayer && spawnedEnemies.Count < numOfEnemies ) {
			spawnCounter += Time.deltaTime;
			if (spawnCounter > spawnInterval) {
				Debug.Log(spawnedEnemies.Count);
				foreach ( SpawnPoint spawnPoint in spawnPoints ) {
					GameObject newEnemyObject = Instantiate( enemyPrefabs[ Random.Range( 0, enemyPrefabs.Count ) ], spawnPoint.transform );
					Enemy newEnemy = newEnemyObject.GetComponent<Enemy>();
					newEnemy.spawner = this;
					newEnemy.SetSpeed( enemySpeed );
					newEnemy.transform.parent = transform.parent;
					spawnedEnemies.Add( newEnemyObject );

				}

				spawnCounter = 0.0f;
			}
		}

		if ( killedAllEnemies ) {
			player.virtualCameraController.shouldMove = true;
		}
	}

	private void OnTriggerEnter( Collider other ) {

		Player player;
		if ( other.TryGetComponent<Player>( out player ) ) {
			reachedByPlayer = true;
			player.virtualCameraController.shouldMove = false;
		}
	}

	public void RefreshSpawnPoints() {

		spawnPoints = GetComponentsInChildren<SpawnPoint>();

	}

	public void CreateSpawnPoint() {
		GameObject newSpawnPoint = Instantiate( spawnPointPrefab );
#if UNITY_EDITOR
		Undo.RegisterCreatedObjectUndo( newSpawnPoint, "Create new Enemy Spawn Point" );
		//Undo.RecordObject( newSpawnPoint, "Create new Enemy Spawn Point" );
#endif //UNITY_EDITOR
		newSpawnPoint.transform.parent = transform;

		RefreshSpawnPoints();

	}

	public void DestroyEnemy( Enemy destroyedEnemy ) {
		Destroy( destroyedEnemy );

		if ( spawnedEnemies.Count == 0 ) {
			killedAllEnemies = true;
		}
	}

}
