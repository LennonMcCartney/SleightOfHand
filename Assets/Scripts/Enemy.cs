using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shape {
	CIRCLE,
	SQUARE,
	TRIANGLE,
	NONE
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

	//public Shape ShieldShape { get; private set; }

	private Shape shieldShape = Shape.NONE;

	//public bool HasShield { get; set; }

	//private bool hasShield = false;

	public Player Player { get; set; }

	public EnemySpawner Spawner { get; set; }

	private string currentAnimation;

	private string[] shapeNames = { "Circle", "Square", "Triangle", "" };
	private string[] actionNames = { "Movement", "Attack", "Death" };

	private string[] shieldedNames = { "_Shielded_Circle", "_Shielded_Square", "_Shielded_Triangle", "" };


	private void Start() {
		mainCamera = Camera.main;

		animator = GetComponentInChildren<Animator>();

		Player = FindObjectOfType<Player>();

		action = Action.MOVEMENT;

		//ShieldShape = Shape.NONE;
		//hasShield = true;

	}

	private void Update() {
		//transform.LookAt( mainCamera.transform.position );
		transform.LookAt( Player.transform.position );
		//transform.Rotate( 0, 180, 0 );
		Vector3 newEulerAngles = new Vector3();
		newEulerAngles.y = transform.eulerAngles.y;
		transform.eulerAngles = newEulerAngles;

		//Debug.Log( "Shield Shape > " + shieldShape );

		string newAnimation = shapeNames[ (int)Shape ] + "Monster_" + actionNames[ (int)action ] + shieldedNames[ (int)shieldShape ];
		if ( currentAnimation != newAnimation ) {
			currentAnimation = newAnimation;
			animator.Play( currentAnimation );
		}

		//if ( Vector2.Distance() ) {

		//}

	}

	public void HitBySpell( Shape spellShape ) {

		Debug.Log( "HitBySpell " + spellShape );

		if ( shieldShape == Shape.NONE ) {
			if ( spellShape == Shape || spellShape == Shape.NONE ) {
				Debug.Log( "DestroyEnemy" );
				Spawner.DestroyEnemy( this );
			}
		} else {
			if ( spellShape == shieldShape || spellShape == Shape.NONE ) {
				shieldShape = Shape.NONE;
			}
		}

	}

	public void HasShield() {
		int shapeNum = Random.Range( 0, 3 );
		switch (shapeNum) {
			case 0:
				Debug.Log( "Shield CIRCLE" );
				shieldShape = Shape.CIRCLE;
				break;
			case 1:
				Debug.Log( "Shield SQUARE" );
				shieldShape = Shape.SQUARE;
				break;
			case 2:
				Debug.Log( "Shield TRIANGLE" );
				shieldShape = Shape.TRIANGLE;
				break;
		}
	}
}
