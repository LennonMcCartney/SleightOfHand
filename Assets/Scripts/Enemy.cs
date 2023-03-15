using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Shape
{
	CIRCLE,
	SQUARE,
	TRIANGLE
}

public class Enemy : MonoBehaviour
{
	private Camera mainCamera;

	private NavMeshAgent navMeshAgent;

	private Animator animator;

	[SerializeField] private Shape shape;

	private void Start() {
		mainCamera= Camera.main;

		navMeshAgent = GetComponent<NavMeshAgent>();

		animator = GetComponentInChildren<Animator>();

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

		if ( spellShape == shape ) {
			Destroy( gameObject );

		}

	}
}
