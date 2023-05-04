using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shape {
	CIRCLE,
	SQUARE,
	TRIANGLE
}

public enum Action {
	MOVEMENT,
	ATTACK,
	DEATH
}

public class Enemy : MonoBehaviour
{
	public float Speed { get; set; } = 1.0f;

	private Camera mainCamera;

	private Animator animator;

	private Action action;

	[field:SerializeField]
	public Shape Shape { get; private set; }

	public Shape ShieldShape { get; private set; }

	public bool HasShield { get; private set; }

	public Player Player { get; set; }

	public EnemySpawner Spawner { get; set; }

	private string currentAnimation;

	private string[] shapeNames = { "Circle", "Square", "Triangle" };
	private string[] actionNames = { "Movement", "Attack", "Death" };


	private void Start() {
		mainCamera = Camera.main;

		animator = GetComponentInChildren<Animator>();

		Player = FindObjectOfType<Player>();

		action = Action.MOVEMENT;

	}

	private void Update() {
		transform.LookAt( Player.VirtualCameraController.transform.position );
		//transform.Rotate( 0, 180, 0 );
		//Vector3 newEulerAngles = new Vector3();
		//newEulerAngles.y = transform.eulerAngles.y;
		//transform.eulerAngles = newEulerAngles;

		string newAnimation = shapeNames[ (int)Shape ] + "Monster_" + actionNames[ (int)action ];
		if ( currentAnimation != newAnimation ) {
			currentAnimation = newAnimation;
			animator.Play( currentAnimation );
		}

		//if ( Vector2.Distance() ) {

		//}

	}

	public void HitBySpell( Shape spellShape ) {

		Debug.Log( "HitBySpell " + spellShape );

		if ( spellShape == Shape ) {
			Debug.Log( "DestroyEnemy" );
			Spawner.DestroyEnemy( this );
		}

	}
}
