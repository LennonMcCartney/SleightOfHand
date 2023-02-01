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

	[SerializeField] private List<Vector2> targetGridPositions;
	[SerializeField] private Vector2 targetGridSize = new Vector2(4,4);
	//[SerializeField] private Vector2 targetSize;

	[SerializeField] private bool pinching = false;

	[SerializeField] private List<Vector2> targetPoints;
	[SerializeField] private List<Vector3> drawPoints;

	LineRenderer lineRenderer;

	private float counter = 0.0f;

	private void Start() {
		lineRenderer = GetComponent<LineRenderer>();

		//for ( int j = 0; j < targetGridSize.y; j++ ) {
			//for ( int i = 0; i < targetGridSize.x; i++ ) {
				//targetPoints.Add( new Vector2( i, j ) );
			//}
		//}
		for ( float j = 0.7f; j >= -0.7f; j -= 0.2f ) {
			for ( float i = -0.7f; i <= 0.7f; i += 0.2f ) {
				targetGridPositions.Add( new Vector2( i, j ) );
			}
		}

		for ( int i = 0; i < targetGridPositions.Count; i++ ) {
			Debug.Log( targetGridPositions[i] );
		}
	}

	public void FixedUpdate() {
		if ( pinching ) {
			drawBackground.SetActive( true );
			counter += Time.deltaTime;
			if ( counter > 0.01f ) {
				Vector2 newDrawPoint = new Vector2( pinchDetector.transform.localPosition.x, pinchDetector.transform.localPosition.y );

				Vector2 newTargetPoint = newDrawPoint;
				newTargetPoint.y -= 1.5f;
				targetPoints.Add( newTargetPoint );

				for ( int i = 0; i < targetGridPositions.Count; i++ ) {
					if ( Vector2.Distance( targetGridPositions[i], newTargetPoint ) < 0.1f ) {
						Debug.Log( Vector2.Distance( targetGridPositions[0], newTargetPoint ) + " from " + targetGridPositions[i] );
					}
				}
				//Debug.Log( newTargetPoint );

				drawPoints.Add( new Vector3( newDrawPoint.x, newDrawPoint.y, 2 ) );
				counter = 0.0f;
			}
		} else {
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
		drawPoints.Clear();
		pinching = false;
	}
}
