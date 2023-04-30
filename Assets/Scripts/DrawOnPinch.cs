using Leap.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawOnPinch : MonoBehaviour {

	private enum Handedness {
		LEFT,
		RIGHT
	}

	[SerializeField] private float targetScale;

	[SerializeField] private float circleTargetsRadius;

	[SerializeField] private GameObject player;

	[SerializeField] private PinchDetector pinchDetectorLeft;
	[SerializeField] private PinchDetector pinchDetectorRight;

	[SerializeField] private GameObject circleTargets;
	[SerializeField] private GameObject squareTargets;
	[SerializeField] private GameObject triangeTargets;

	[SerializeField] private GameObject targetPrefab;

	private bool pinchingLeft = false;
	private bool pinchingRight = false;

	private List<Vector3> leftDrawPoints = new List<Vector3>();
	private List<Vector3> rightDrawPoints = new List<Vector3>();
	private List<Vector2> collisionPoints = new List<Vector2>();

	private List<Vector2> circleTargetPoints = new List<Vector2>();
	private List<Vector2> hitCircleTargetPoints = new List<Vector2>();

	private List<Vector2> squareTargetPoints = new List<Vector2>();
	private List<Vector2> hitSquareTargetPoints = new List<Vector2>();

	[SerializeField] private LineRenderer leftLineRenderer;
	[SerializeField] private LineRenderer rightLineRenderer;

	private int enemyLayerMask;

	private float counter = 0.0f;

	private bool hitAllCircleTargets = false;
	private bool hitAllSquareTargets = false;

	private bool failedAtCircle = false;
	private bool failedAtSquare = false;
	private bool failedAtTriangle = false;

	public void DebugKillEnemy() {
		FireSpell( Shape.CIRCLE );
		FireSpell( Shape.SQUARE );
		FireSpell( Shape.TRIANGLE );
	}

	public static float Round( float value, int digits ) {
		float mult = Mathf.Pow( 10.0f, digits );
		return Mathf.Round( value * mult ) / mult;
	}

	private void FireSpell( Shape spellShape ) {
		Debug.Log( "Fire > " + spellShape );
		if ( Physics.Raycast( player.transform.position, player.transform.forward, out RaycastHit raycastHit, 1000000.0f, enemyLayerMask ) ) {
			if ( raycastHit.transform.gameObject.TryGetComponent<Enemy>( out Enemy hitEnemy ) ) {
				hitEnemy.HitBySpell( spellShape );
			}
		}
	}

	private void Start() {
		enemyLayerMask = LayerMask.GetMask( "Enemy" );

		circleTargets.SetActive( false );
		squareTargets.SetActive( false );
		triangeTargets.SetActive( false );

		GenerateCircleTargets();
		GenerateSquareTargets();
		//GenerateTriangleTargets();

	}

	public void FixedUpdate() {
		//Debug.DrawLine( player.transform.position, player.transform.position + player.transform.forward * 1000.0f, Color.green );

		if ( pinchingLeft ) {
			Pinching( Handedness.LEFT, pinchDetectorLeft.transform );
		}

		if ( pinchingRight ) {
			Pinching( Handedness.RIGHT, pinchDetectorRight.transform );
		}

		rightLineRenderer.positionCount = rightDrawPoints.Count;
		for (int i = 0; i < rightDrawPoints.Count; i++) {
			rightLineRenderer.SetPosition( i, rightDrawPoints[ i ] );
		}

		leftLineRenderer.positionCount = leftDrawPoints.Count;
		for ( int i = 0; i < leftDrawPoints.Count; i++ ) {
			leftLineRenderer.SetPosition( i, leftDrawPoints[i] );
		}
	}

	private void Pinching( Handedness handedness, Transform pinchDetectorTransform ) {
		circleTargets.SetActive( true );
		squareTargets.SetActive( true );
		triangeTargets.SetActive( true );

		counter += Time.deltaTime;
		if ( counter > 0.01f ) {
			//Vector2 newDrawPoint = new Vector2(pinchDetector.transform.localPosition.x, pinchDetector.transform.localPosition.y);
			Vector2 newPoint = new Vector2( pinchDetectorTransform.localPosition.x, pinchDetectorTransform.localPosition.y ) * 3.0f;

			//Vector2 newCollisionPoint = newDrawPoint;
			//newCollisionPoint.y -= 1.5f;
			collisionPoints.Add( newPoint );

			//Debug.Log( newPoint );

			CheckNewPointCircle( newPoint );
			CheckNewPointSquare( newPoint );

			switch ( handedness ) {
				case Handedness.LEFT:
					leftDrawPoints.Add( new Vector3( newPoint.x, newPoint.y, 2 ) );
					break;
				case Handedness.RIGHT:
					rightDrawPoints.Add( new Vector3( newPoint.x, newPoint.y, 2 ) );
					break;
			}

			counter = 0.0f;
		}
	}

	private void GenerateCircleTargets() {
		for (int i = 0; i < 32; i++) {
			Vector3 newTargetPosition = new Vector3( Round( circleTargetsRadius * Mathf.Sin( ( Mathf.PI / 16 ) * i ), 6 ), Round( circleTargetsRadius * Mathf.Cos( ( Mathf.PI / 16 ) * i ) + 0.1f, 6 ) );

			GameObject newTarget = Instantiate( targetPrefab );
			newTarget.transform.parent = circleTargets.transform;
			newTarget.transform.localPosition = newTargetPosition;
			newTarget.transform.forward = player.transform.forward;
			newTarget.transform.localScale = new Vector3( targetScale, targetScale, targetScale );

			circleTargetPoints.Add( newTargetPosition );
		}
	}

	private void GenerateSquareTargets() {
		for ( int i = -8; i < 8; i++ ) {
			float thisI = i;
			Vector3 newTargetPosition = new Vector3( thisI / 10, 0.8f + 0.1f );
			//Debug.Log( newTargetPosition );
			GameObject newTarget = Instantiate( targetPrefab );
			newTarget.transform.parent = squareTargets.transform;
			newTarget.transform.localPosition = newTargetPosition;
			newTarget.transform.forward = player.transform.forward;
			newTarget.transform.localScale = new Vector3( targetScale, targetScale, targetScale );

			squareTargetPoints.Add( newTargetPosition );
		}

		for ( int i = -8; i < 8; i++ ) {
			float thisI = i;
			Vector3 newTargetPosition = new Vector3( -0.8f, thisI / 10 + 0.1f );
			//Debug.Log( newTargetPosition );
			GameObject newTarget = Instantiate( targetPrefab );
			newTarget.transform.parent = squareTargets.transform;
			newTarget.transform.localPosition = newTargetPosition;
			newTarget.transform.forward = player.transform.forward;
			newTarget.transform.localScale = new Vector3( targetScale, targetScale, targetScale );

			squareTargetPoints.Add( newTargetPosition );
		}

		for ( int i = -8; i < 8; i++ ) {
			float thisI = i;
			Vector3 newTargetPosition = new Vector3( 0.8f, thisI / 10 + 0.1f );
			//Debug.Log( newTargetPosition );
			GameObject newTarget = Instantiate( targetPrefab );
			newTarget.transform.parent = squareTargets.transform;
			newTarget.transform.localPosition = newTargetPosition;
			newTarget.transform.forward = player.transform.forward;
			newTarget.transform.localScale = new Vector3( targetScale, targetScale, targetScale );

			squareTargetPoints.Add( newTargetPosition );
		}

		for ( int i = -8; i < 8; i++ ) {
			float thisI = i;
			Vector3 newTargetPosition = new Vector3( thisI / 10, -0.8f + 0.1f );
			//Debug.Log( newTargetPosition );
			GameObject newTarget = Instantiate( targetPrefab );
			newTarget.transform.parent = squareTargets.transform;
			newTarget.transform.localPosition = newTargetPosition;
			newTarget.transform.forward = player.transform.forward;
			newTarget.transform.localScale = new Vector3( targetScale, targetScale, targetScale );

			squareTargetPoints.Add( newTargetPosition );
		}

		//int x = -1;
		//int y = -1;

		////point to the right
		//int dx = 1;
		//int dy = 0;

		//for ( int side = 0; side < 4; ++side ) {
		//	for ( int i = 1; i < 10; ++i ) {
		//		Vector3 newCircleTargetPosition = new Vector3( x, y );

		//		GameObject newCircleTarget = Instantiate( targetPrefab );
		//		newCircleTarget.transform.parent = circleTargets.transform;
		//		newCircleTarget.transform.localPosition = newCircleTargetPosition;
		//		newCircleTarget.transform.localScale = new Vector3( targetScale, targetScale, targetScale );

		//		circleTargetPoints.Add( newCircleTargetPosition );

		//		x += dx;
		//		y += dy;
		//	}
		//	//turn right
		//	int t = dx;
		//	dx = -dy;
		//	dy = t;
		//}

		//for (int i = 0; i < 4; i++ ) {
		//	for (int j = 0; j < 10; j++) {
		//		Vector3 newSquareTargetPosition = new Vector3(i, j);

		//		GameObject newSquareTarget = Instantiate(targetPrefab);
		//		newSquareTarget.transform.parent = squareTargets.transform;
		//		newSquareTarget.transform.localPosition = newSquareTargetPosition;
		//		newSquareTarget.transform.localScale = new Vector3(targetScale, targetScale, targetScale);

		//		squareTargetPoints.Add(newSquareTargetPosition);
		//          }
		//}
	}

	private void GenerateTriangleTargets() {
	}

	private void CheckCircle() {
		foreach ( Vector2 circleTargetPoint in circleTargetPoints ) {
			if ( !hitCircleTargetPoints.Contains( circleTargetPoint ) ) {
				hitAllCircleTargets = false;
				break;
			}
		}
	}

	private void CheckSquare() {
		//Debug.Log( "hitSquareTargetPoints > " + hitSquareTargetPoints );
		foreach ( Vector2 squareTargetPoint in squareTargetPoints ) {
			if ( !hitSquareTargetPoints.Contains( squareTargetPoint ) ) {
				hitAllSquareTargets = false;
				break;
			}
		}
	}

	private void CheckNewPointCircle( Vector2 newCollisionPoint ) {
		failedAtCircle = true;

		for ( int i = 0; i < circleTargetPoints.Count; i++ ) {
			//bool testBool = false;
			if ( Vector2.Distance( circleTargetPoints[i], newCollisionPoint ) < 0.2f ) {
				failedAtCircle = false;
				if ( !hitCircleTargetPoints.Contains( circleTargetPoints[i] ) ) {
					hitCircleTargetPoints.Add( circleTargetPoints[i] );
				}
			}
		}

		if ( failedAtCircle ) {
			//circleTargets.SetActive(false);
		}
	}

	private void CheckNewPointSquare( Vector2 newCollisionPoint ) {
		failedAtSquare = true;

		for ( int i = 0; i < squareTargetPoints.Count; i++ ) {
			if ( Vector2.Distance( squareTargetPoints[i], newCollisionPoint ) < 0.2f ) {
				failedAtSquare = false;
				if ( !hitSquareTargetPoints.Contains( squareTargetPoints[i] ) ) {
					hitSquareTargetPoints.Add( squareTargetPoints[i] );
				}
			}
		}

		if ( failedAtSquare ) {

		}

	}

	public void PinchLeft() {
		hitAllCircleTargets = true;
		hitAllSquareTargets = true;
		failedAtCircle = false;
		pinchingLeft = true;
	}

	public void PinchRight() {
		hitAllCircleTargets = true;
		hitAllSquareTargets = true;
		failedAtCircle = false;
		pinchingRight = true;
	}

	public void EndPinch() {
		CheckCircle();
		CheckSquare();

		if ( hitAllCircleTargets && !failedAtCircle ) {
			FireSpell( Shape.CIRCLE );
		} else if ( hitAllSquareTargets && !failedAtSquare ) {
			FireSpell( Shape.SQUARE );
		}

		leftDrawPoints.Clear();
		rightDrawPoints.Clear();
		collisionPoints.Clear();
		hitCircleTargetPoints.Clear();
		hitSquareTargetPoints.Clear();

		pinchingLeft = false;
		pinchingRight = false;

		circleTargets.SetActive( false );
		squareTargets.SetActive( false );
		triangeTargets.SetActive( false );
	}

	public void EndPinchLeft() {
		foreach ( Vector2 circleTargetPoint in circleTargetPoints ) {
			if ( !hitCircleTargetPoints.Contains( circleTargetPoint ) ) {
				hitAllCircleTargets = false;
				break;
			}
		}

		if ( hitAllCircleTargets && !failedAtCircle ) {
			FireSpell( Shape.CIRCLE );
		}

		leftDrawPoints.Clear();
		rightDrawPoints.Clear();
		collisionPoints.Clear();
		hitCircleTargetPoints.Clear();

		pinchingLeft = false;

		circleTargets.SetActive( false );
		squareTargets.SetActive( false );
		triangeTargets.SetActive( false );
	}

	public void EndPinchRight() {
		foreach ( Vector2 circleTargetPoint in circleTargetPoints ) {
			if ( !hitCircleTargetPoints.Contains( circleTargetPoint ) ) {
				hitAllCircleTargets = false;
				break;
			}
		}

		if ( hitAllCircleTargets && !failedAtCircle ) {
			FireSpell( Shape.CIRCLE );
		}

		leftDrawPoints.Clear();
		rightDrawPoints.Clear();
		collisionPoints.Clear();
		hitCircleTargetPoints.Clear();

		pinchingRight = false;

		circleTargets.SetActive( false );
		squareTargets.SetActive( false );
		triangeTargets.SetActive( false );
	}

}
