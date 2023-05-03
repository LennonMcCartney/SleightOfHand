using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shape
{
	CIRCLE,
	SQUARE,
	TRIANGLE
}

[RequireComponent( typeof( Animator ) )]
public class Enemy : MonoBehaviour
{
	
	public float Speed { get; set; } = 1.0f;

	private Camera mainCamera;

	private Animator animator;

	//[SerializeField] private Shape shape;
	[field:SerializeField]
	public Shape Shape { get; private set; }

	public Player Player { get; set; }

	public EnemySpawner Spawner { get; set; }

	private void Start() {
		mainCamera = Camera.main;

		animator = GetComponentInChildren<Animator>();

		Player = FindObjectOfType<Player>();

		switch ( Shape ) {
			case Shape.CIRCLE:
				animator.Play( "CircleMonster_Movement" );
				break;
			case Shape.SQUARE:
				animator.Play( "SquareMonster_Movement" );
				break;
			case Shape.TRIANGLE:
				animator.Play( "TriangleMonster_Movement" );
				break;
			default:
				Debug.Log( "Invalid shape > " + Shape );
				break;
		}

	}

	private void Update() {
		transform.LookAt( mainCamera.transform );
		transform.Rotate( 0, 180, 0 );
		Vector3 newEulerAngles = new Vector3();
		newEulerAngles.y = transform.eulerAngles.y;
		transform.eulerAngles = newEulerAngles;

	}

	public void HitBySpell( Shape spellShape ) {

		Debug.Log( "HitBySpell " + spellShape );

		if ( spellShape == Shape ) {
			Debug.Log( "DestroyEnemy" );
			Spawner.DestroyEnemy( this );
			//Destroy( gameObject );

		}

	}
}
