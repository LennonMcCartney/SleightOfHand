using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareEnemy : MonoBehaviour {

	private Enemy enemy;

	private void Start() {
		enemy = GetComponent<Enemy>();

	}

}
