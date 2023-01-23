using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PinchTest : MonoBehaviour {

	public bool pinching = false;

	//[SerializeField] private List<Vector2> drawPoints;
	[SerializeField] private List<Vector3> drawPoints;

	LineRenderer lineRenderer;

	private float counter = 0.0f;

	private void Start() {
		lineRenderer = GetComponent<LineRenderer>();

	}

	public void FixedUpdate() {
		if ( pinching ) {
			counter += Time.deltaTime;
			if ( counter > 0.01f ) {
				drawPoints.Add( transform.localPosition );
				//drawPoints.Add( new Vector2( transform.localPosition.x, transform.localPosition.y ) );
				//Debug.Log( new Vector2( transform.localPosition.x, transform.localPosition.y ) );
				counter = 0.0f;
			}
			//Debug.Log( "Pinch at " + new Vector2( transform.localPosition.x, transform.localPosition.y ) );
		}

		lineRenderer.positionCount = drawPoints.Count;
		for ( int i = 0; i < drawPoints.Count; i++ ) {
			lineRenderer.SetPosition(i, drawPoints[i] );
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
