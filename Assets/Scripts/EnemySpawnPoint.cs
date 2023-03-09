using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    //[SerializeField] List<Shape> enemyShapes;

    //[SerializeField] int numOfEnemies;

    public void SpawnHere( GameObject enemyPrefab )
    {
        GameObject newEnemy = Instantiate( enemyPrefab, transform );

    }

}
