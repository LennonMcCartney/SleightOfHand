using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shape
{
	CIRCLE,
	SQUARE,
	TRIANGLE
}

public class Enemy : MonoBehaviour
{
	private float speed = 1.0f;

	private Camera mainCamera;

	private Animator animator;

	[SerializeField] private Shape shape;

	private Player player;

	[HideInInspector]
	public EnemySpawner spawner;

	public float Speed
    {
		get { return speed; }
		set { speed = value; }
    }

	public Player Player
    {
		get { return player; }
		set { player = value; }
    }

	private void Start() {
		mainCamera = Camera.main;

		animator = GetComponentInChildren<Animator>();

		player = FindObjectOfType<Player>();

		switch ( shape ) {
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
				Debug.Log( "Invalid shape > " + shape );
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

		//Debug.Log( "HitBySpell" );

		if ( spellShape == shape ) {
			//Debug.Log( "DestroyEnemy" );
			spawner.DestroyEnemy( this );
			//Destroy( gameObject );

		}

	}
}
