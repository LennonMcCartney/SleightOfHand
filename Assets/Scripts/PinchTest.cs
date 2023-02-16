using Leap.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PinchTest : MonoBehaviour {

	[SerializeField] private GameObject playerCapsule;

	[SerializeField] private PinchDetector pinchDetector;

	[SerializeField] private GameObject drawBackground;

	[SerializeField] private bool pinching = false;

	//[SerializeField] private List<Vector2> targetGridPositions;
	//[SerializeField] private Vector2 targetGridSize = new Vector2( 4, 4 );

	//[SerializeField] private List<Vector2> hitTargetGridPositions;
	//[SerializeField] private List<Vector2> targetPoints;
	[SerializeField] private List<Vector3> drawPoints;

	[SerializeField] private List<Vector2> collisionPoints;

	[SerializeField] private List<Vector2> circleTargetPoints;

	[SerializeField] private List<Vector2> hitCircleTargetPoints;

	LineRenderer lineRenderer;

	private float counter = 0.0f;

	private void Start() {
		lineRenderer = GetComponent<LineRenderer>();

		circleTargetPoints = new List<Vector2>() {
			new Vector2( 0.0f, 0.7f ),
			new Vector2( -0.4949747f, 0.4949747f ),
			new Vector2( -0.7f, 0.0f ),
			new Vector2( -0.4949747f, -0.4949747f ),
			new Vector2( 0.0f, -0.7f ),
			new Vector2( 0.4949747f, -0.4949747f ),
			new Vector2( 0.7f, 0.0f ),
			new Vector2( 0.4949747f, 0.4949747f )
		};

		//for ( float j = 0.7f; j >= -0.7f; j -= 0.2f ) {
		//for ( float i = -0.7f; i <= 0.7f; i += 0.2f ) {
		//targetGridPositions.Add( new Vector2( Mathf.Round( i * 10.0f ) * 0.1f, Mathf.Round( j * 10.0f ) * 0.1f ) );
		//}
		//}

		//for ( int i = 0; i < targetGridPositions.Count; i++ ) {
		//Debug.Log( targetGridPositions[i] );
		//}
	}

	public void FixedUpdate() {
		if (pinching) {
			drawBackground.SetActive( true );
			counter += Time.deltaTime;
			if (counter > 0.01f) {
				Vector2 newDrawPoint = new Vector2( pinchDetector.transform.localPosition.x, pinchDetector.transform.localPosition.y );

				Vector2 newCollisionPoint = newDrawPoint;
				newCollisionPoint.y -= 1.5f;
				collisionPoints.Add( newCollisionPoint );

				for (int i = 0; i < circleTargetPoints.Count; i++) {
					if (Vector2.Distance( circleTargetPoints[ i ], newCollisionPoint ) < 0.1f && !hitCircleTargetPoints.Contains( circleTargetPoints[ i ] )) {
						hitCircleTargetPoints.Add( circleTargetPoints[ i ] );
					}
				}

				//for ( int i = 0; i < targetGridPositions.Count; i++ ) {
				//if ( Vector2.Distance( targetGridPositions[i], newTargetPoint ) < 0.1f ) {
				//if ( !hitTargetGridPositions.Contains( targetGridPositions[i] ) ) {
				//hitTargetGridPositions.Add( targetGridPositions[i] );
				//}
				//Debug.Log( Vector2.Distance( targetGridPositions[0], newTargetPoint ) + " from " + targetGridPositions[i] );
				//}
				//}

				drawPoints.Add( new Vector3( newDrawPoint.x, newDrawPoint.y, 2 ) );
				counter = 0.0f;
			}
		} else if ( drawBackground.activeInHierarchy ) {
			drawBackground.SetActive( false );
			
		}

		lineRenderer.positionCount = drawPoints.Count;
		for ( int i = 0; i < drawPoints.Count; i++ ) {
			lineRenderer.SetPosition( i, drawPoints[i] );
		}
	}

	public void Pinch() {
		pinching = true;
	}

	public void EndPinch() {

		bool isACircle = true;

		foreach (Vector2 circleTargetPoint in circleTargetPoints) {
			if (!hitCircleTargetPoints.Contains( circleTargetPoint )) {
				isACircle = false;
				break;
			}
		}

		if (isACircle) {
			Debug.Log( "Circle" );
		} else {
			Debug.Log( "Not Circle" );
		}

		drawPoints.Clear();
		collisionPoints.Clear();
		hitCircleTargetPoints.Clear();
		pinching = false;
	}
}
