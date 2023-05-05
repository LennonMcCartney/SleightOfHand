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

	public Action action;

	[field:SerializeField]
	public Shape Shape { get; private set; }

	private Shape shieldShape = Shape.NONE;

	public Player Player { get; set; }

	public EnemySpawner Spawner { get; set; }

	private string currentAnimation;

	private string[] shapeNames = { "Circle", "Square", "Triangle", "" };
	private string[] actionNames = { "Movement", "Attack", "Death" };

	private string[] shieldedNames = { "_Shielded_Circle", "_Shielded_Square", "_Shielded_Triangle", "" };

	[HideInInspector] public AudioSource audioSource;

	[SerializeField] public AudioClip idleSound;
	[SerializeField] public AudioClip attackSound;

	[SerializeField] private float attackCooldown = 1.0f;
	private float attackTimer = 0.0f;

	public bool reachedPlayer = false;

	private void Start() {
		mainCamera = Camera.main;

		audioSource = GetComponent<AudioSource>();
		audioSource.clip = idleSound;
		audioSource.Play();

		animator = GetComponentInChildren<Animator>();

		Player = FindObjectOfType<Player>();

		action = Action.MOVEMENT;

		attackTimer = attackCooldown;

		UpdateAnimation();

	}

	private void Update() {
		//transform.LookAt( mainCamera.transform.position );
		transform.LookAt( Player.transform.position );
		//transform.Rotate( 0, 180, 0 );
		Vector3 newEulerAngles = new Vector3();
		newEulerAngles.y = transform.eulerAngles.y;
		transform.eulerAngles = newEulerAngles;

		//Debug.Log( "Shield Shape > " + shieldShape );

		UpdateAnimation();

		if ( Vector3.Distance( transform.position, Player.transform.position ) < 3.0f ) {
			reachedPlayer = true;
			if ( attackTimer >= attackCooldown ) {
				audioSource.clip = attackSound;
				action = Action.ATTACK;
				Player.Hit();
				attackTimer = 0.0f;
			} else {
				attackTimer += Time.deltaTime;
			}
		}

	}

	private void UpdateAnimation() {
		string newAnimation = shapeNames[ (int)Shape ] + "Monster_" + actionNames[ (int)action ] + shieldedNames[ (int)shieldShape ];
		if (currentAnimation != newAnimation) {
			currentAnimation = newAnimation;
			animator.Play( currentAnimation );
		}
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
