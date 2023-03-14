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
	//[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Animator animator;

	private Camera mainCamera;

	[SerializeField] private Shape shape;

	private void Start() {
		mainCamera= Camera.main;

		switch ( shape ) {
			case Shape.CIRCLE:
				animator.Play( "CircleMonster_Movement" );
				break;
			case Shape.SQUARE:
				break;
			case Shape.TRIANGLE:
				break;
			default:
				break;
		}

		//animator.Play("CircleMonster_Movement");

	}

	private void Update() {
		transform.LookAt( mainCamera.transform );
		transform.Rotate( 0, 180, 0 );
		Vector3 newEulerAngles = transform.eulerAngles;
		newEulerAngles.x = 0;
		newEulerAngles.z = 0;
		transform.eulerAngles = newEulerAngles;

	}

	public void Hit( Shape spellShape ) {
		if ( spellShape == shape ) {

		}

		Destroy(gameObject);

	}
}
