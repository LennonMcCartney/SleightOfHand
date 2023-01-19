using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchTest : MonoBehaviour {

	public bool pinching = false;

	[SerializeField] private List<Vector2> drawPoints;

	LineRenderer lineRenderer;

	private float counter = 0.0f;

	public void FixedUpdate() {
		if ( pinching ) {
			counter += Time.deltaTime;
			if ( counter > 0.05f ) {
				drawPoints.Add( new Vector2( transform.localPosition.x, transform.localPosition.y ) );
				//lineRenderer.
				Debug.Log( new Vector2( transform.localPosition.x, transform.localPosition.y ) );
				counter = 0.0f;
			}
			//Debug.Log( "Pinch at " + new Vector2( transform.localPosition.x, transform.localPosition.y ) );
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
