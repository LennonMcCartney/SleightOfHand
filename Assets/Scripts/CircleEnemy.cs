using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CircleEnemy : MonoBehaviour {
    
    private Enemy enemy;

    private NavMeshAgent navMeshAgent;

    void Start() {
        enemy = GetComponent<Enemy>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = enemy.Speed;

    }

    void Update() {
        navMeshAgent.SetDestination( enemy.Player.transform.position );

    }

}
