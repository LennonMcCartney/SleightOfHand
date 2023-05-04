using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SquareEnemy : MonoBehaviour {

	private Enemy enemy;

	private NavMeshAgent navMeshAgent;

	[SerializeField] private float movementCooldown;
	[SerializeField] private float movementTime;

	private float timer = 0.0f;

	private void Start() {
		enemy = GetComponent<Enemy>();

		navMeshAgent = GetComponent<NavMeshAgent>();
		navMeshAgent.speed = 0.0f;
		navMeshAgent.acceleration = 15.0f;

	}

	private void Update() {
		navMeshAgent.SetDestination( enemy.Player.transform.position );

		timer += Time.deltaTime;

		if ( timer > movementCooldown ) {
			//Debug.Log( "movementCooldown" );
			navMeshAgent.speed = enemy.Speed * 15.0f;

			if ( timer > movementCooldown + movementTime ) {
				//Debug.Log( "movementTime" );
				navMeshAgent.speed = 0.0f;
				timer = 0.0f;
			}
		}

		Debug.Log( navMeshAgent.speed );

	}

}
