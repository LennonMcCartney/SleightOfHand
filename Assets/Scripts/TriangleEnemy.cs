using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemy : MonoBehaviour {

	private Enemy enemy;

	private float flightTimer = 0.0f;
	private float sinTimer = 0.0f;

	public float FlightTime { get; set; }

	private float baseHeight;

	private void Start() {
		enemy = GetComponent<Enemy>();
	}

	private void Update() {

		flightTimer += Time.deltaTime;

		if ( flightTimer < FlightTime ) {
			Vector3 position = transform.position;
			position.y += Time.deltaTime * enemy.Speed;
			baseHeight = position.y;
			transform.position = position;
		} else {
			Debug.Log( Mathf.Sin( sinTimer ) );
			Vector3 position = transform.position;
			position.y = baseHeight + Mathf.Sin( sinTimer / 2.0f ) / 3.0f;
			transform.position = position;

			sinTimer += Time.deltaTime;
		}
	}

}
