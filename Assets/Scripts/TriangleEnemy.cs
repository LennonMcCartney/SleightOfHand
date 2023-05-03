using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemy : MonoBehaviour {

    private Enemy enemy;

	private float timer = 0.0f; 

	private void Start() {
		enemy = GetComponent<Enemy>();
	}
	
	private void Update() {
		
		timer += Time.deltaTime;

		if ( timer < 3) {
			Vector3 position = transform.position;
			position.y += Time.deltaTime;
			transform.position = position;
		}
	}

}
