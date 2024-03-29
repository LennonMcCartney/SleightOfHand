using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CircleEnemy : MonoBehaviour {

	private Enemy enemy;

	private NavMeshAgent navMeshAgent;
	
	private void Start() {
		enemy = GetComponent<Enemy>();

		navMeshAgent = GetComponent<NavMeshAgent>();
		navMeshAgent.speed = enemy.Speed;

	}

	private void Update() {
		navMeshAgent.SetDestination( enemy.Player.transform.position );

		if ( enemy.reachedPlayer ) {
			navMeshAgent.speed = 0.0f;
		}

	}

}
