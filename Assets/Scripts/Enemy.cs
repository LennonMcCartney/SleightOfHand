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

	[field: SerializeField]
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

		action = Action.ATTACK;

		//switch ( Shape ) {
		//	case Shape.CIRCLE:
		//		animator.Play( "CircleMonster_Movement" );
		//		break;
		//	case Shape.SQUARE:
		//		animator.Play( "SquareMonster_Movement" );
		//		break;
		//	case Shape.TRIANGLE:
		//		animator.Play( "TriangleMonster_Movement" );
		//		break;
		//	default:
		//		Debug.Log( "Invalid shape > " + Shape );
		//		break;
		//}

	}

	private void Update() {
		transform.LookAt( mainCamera.transform );
		transform.Rotate( 0, 180, 0 );
		Vector3 newEulerAngles = new Vector3();
		newEulerAngles.y = transform.eulerAngles.y;
		transform.eulerAngles = newEulerAngles;

		//Debug.Log( Shape + " > " + shapeNames[ (int)Shape ] );

		if ( currentAnimation != shapeNames[ (int)Shape ] + "_" + actionNames[ (int)action ] ) {
			currentAnimation = shapeNames[ (int)Shape ] + "_" + actionNames[ (int)action ];
			//animator.Play( currentAnimation );
			animator.Play( "SquareMonster_Movement" );
		}

	}

	public void HitBySpell( Shape spellShape ) {

		Debug.Log( "HitBySpell " + spellShape );

		if ( spellShape == Shape ) {
			Debug.Log( "DestroyEnemy" );
			Spawner.DestroyEnemy( this );
		}

	}
}
