using Leap.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchTest : MonoBehaviour {
	
	[SerializeField] private GameObject playerCapsule;

	[SerializeField] private PinchDetector pinchDetector;

	[SerializeField] private GameObject drawBackground;

	[SerializeField] private List<Vector2> targetPositions;
	[SerializeField] private Vector2 targetSize;

	public bool pinching = false;
	[SerializeField] private List<Vector3> drawPoints;

	LineRenderer lineRenderer;

	private float counter = 0.0f;

	private void Start() {
		lineRenderer = GetComponent<LineRenderer>();

	}

	public void FixedUpdate() {
		if ( pinching ) {
			drawBackground.SetActive( true );
			counter += Time.deltaTime;
			if ( counter > 0.01f ) {
				Vector2 newPoint = new Vector2( pinchDetector.transform.localPosition.x, pinchDetector.transform.localPosition.y );
				drawPoints.Add( new Vector3( newPoint.x, newPoint.y, 2 ) );
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
