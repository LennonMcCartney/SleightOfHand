using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemy : MonoBehaviour {

	private Enemy enemy;

	private float flightTimer = 0.0f;
	private float sinTimer = 0.0f;

	public float FlightTime { get; set; }

	private void Start() {
		enemy = GetComponent<Enemy>();
	}

	private void Update() {

		flightTimer += Time.deltaTime;

		if ( flightTimer < FlightTime ) {
			Vector3 position = transform.position;
			position.y += Time.deltaTime * enemy.Speed;
			transform.position = position;
		} else {
			Vector3 position = transform.position;
			position += transform.up * Mathf.Sin( sinTimer * 4.0f ) * 0.004f;
			transform.position = position;

			sinTimer += Time.deltaTime;

			if ( !enemy.reachedPlayer ) {
				transform.position += transform.forward * enemy.Speed * Time.deltaTime;
			}
		}
	}

}
