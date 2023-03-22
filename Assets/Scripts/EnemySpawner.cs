using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class EnemySpawner : MonoBehaviour {

	//public int spawnerId;

	[SerializeField] private float spawnInterval = 5.0f;
	private float spawnCounter = 0.0f;

	[SerializeField] private float enemySpeed;

	private GameObject[] spawnPoints;

	[SerializeField] private List<GameObject> enemyPrefabs;

	[HideInInspector]
	public GameObject spawnPointPrefab;

	[HideInInspector]
	public EnemySpawnManager enemySpawnManager;

	private List<GameObject> spawnedEnemies = new List<GameObject>();

	private void Start() {
		FindSpawnPoints();

	}

	private void Update() {
		Debug.Log(spawnCounter);
		spawnCounter += Time.deltaTime;
		if ( spawnCounter > spawnInterval ) {
			foreach ( GameObject spawnPoint in spawnPoints ) {
				GameObject newEnemy = Instantiate( enemyPrefabs[ Random.Range( 0, enemyPrefabs.Count ) ], spawnPoint.transform );
				newEnemy.GetComponent<Enemy>().SetSpeed( enemySpeed );
				newEnemy.transform.parent = transform.parent;
				spawnedEnemies.Add( newEnemy );

			}

			spawnCounter = 0.0f;
		}
	}

	public void FindSpawnPoints() {
		spawnPoints = GameObject.FindGameObjectsWithTag( "SpawnPoint" );

	}

	public void CreateSpawnPoint() {
		GameObject newSpawnPoint = Instantiate( spawnPointPrefab );
		newSpawnPoint.transform.parent = transform;

		FindSpawnPoints();

		//enemySpawnPoints = FindObjectsOfType<EnemySpawnPoint>();
	}

	//private void OnDestroy() {
		//enemySpawnManager.enemySpawners.Remove(this);
		//enemySpawnManager.RefreshEnemySpawners();

	//}

}
