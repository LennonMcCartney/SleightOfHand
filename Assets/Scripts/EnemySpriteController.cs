using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteController : MonoBehaviour {

	[SerializeField] private Enemy enemy;

	public void EndAttackAnimation() {
		enemy.audioSource.clip = enemy.idleSound;
		enemy.action = Action.MOVEMENT;
	}

}
