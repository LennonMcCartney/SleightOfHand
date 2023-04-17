using Leap.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOnPinch : MonoBehaviour {

	[SerializeField] private float targetScale;

	[SerializeField] private float circleTargetsRadius;

	[SerializeField] private GameObject player;

	[SerializeField] private PinchDetector pinchDetector;

	[SerializeField] private GameObject circleTargets;
	[SerializeField] private GameObject squareTargets;
	[SerializeField] private GameObject triangeTargets;

	[SerializeField] private GameObject targetPrefab;

	private bool pinching = false;

	private List<Vector3> drawPoints = new List<Vector3>();
	private List<Vector2> collisionPoints = new List<Vector2>();
	
	private List<Vector2> circleTargetPoints = new List<Vector2>();
	private List<Vector2> hitCircleTargetPoints = new List<Vector2>();

	private List<Vector2> squareTargetPoints = new List<Vector2>();
	private List<Vector2> hitSquareTargetPoints = new List<Vector2>();

	LineRenderer lineRenderer;

	private float counter = 0.0f;

	private bool hitAllCircleTargets = false;

	private bool failedAtCircle = false;

	public static float Round(float value, int digits)
	{
		float mult = Mathf.Pow(10.0f, digits);
		return Mathf.Round(value * mult) / mult;
	}

	private void FireSpell( Shape spellShape )
    {
		Debug.Log( "Fire " + spellShape );

		if ( Physics.Raycast( player.transform.position, player.transform.position + player.transform.forward * 5.0f, out RaycastHit raycastHit ) )
        {
			if ( raycastHit.transform.gameObject.TryGetComponent<Enemy>( out Enemy hitEnemy ) ) {
				hitEnemy.HitBySpell( spellShape );
			}
		}

    }

	private void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();

		//circleTargets.SetActive(false);
		squareTargets.SetActive(false);
		triangeTargets.SetActive(false);

		GenerateCircleTargets();
		//GenerateSquareTargets();
		//GenerateTriangleTargets();

	}

	public void FixedUpdate()
	{
		Debug.DrawLine( player.transform.position, player.transform.position + player.transform.forward * 5.0f, Color.green );
		//Debug.DrawLine( new Vector3( 0.0f, 1.0f, 0.0f ), new Vector3( 3.0f, 1.0f, 0.0f ), Color.green );

		if ( pinching )
		{
			circleTargets.SetActive( true );
			squareTargets.SetActive( true );
			triangeTargets.SetActive( true );

			counter += Time.deltaTime;
			if ( counter > 0.01f )
			{
				//Vector2 newDrawPoint = new Vector2(pinchDetector.transform.localPosition.x, pinchDetector.transform.localPosition.y);
				Vector2 newPoint = new Vector2( pinchDetector.transform.localPosition.x, pinchDetector.transform.localPosition.y ) * 3.0f;

				//Vector2 newCollisionPoint = newDrawPoint;
				//newCollisionPoint.y -= 1.5f;
				collisionPoints.Add( newPoint );

				Debug.Log( newPoint );

				CheckCircle( newPoint );

				drawPoints.Add( new Vector3( newPoint.x, newPoint.y, 2 ) );
				counter = 0.0f;
			}
		}

		lineRenderer.positionCount = drawPoints.Count;
		for (int i = 0; i < drawPoints.Count; i++)
		{
			lineRenderer.SetPosition(i, drawPoints[i]);
		}
	}

	private void GenerateCircleTargets() {
		for (int i = 0; i < 32; i++) {
			Vector3 newCircleTargetPosition = new Vector3(Round(circleTargetsRadius * Mathf.Sin((Mathf.PI / 16) * i), 6), Round(circleTargetsRadius * Mathf.Cos((Mathf.PI / 16) * i), 6), 0);

			GameObject newCircleTarget = Instantiate(targetPrefab);
			newCircleTarget.transform.parent = circleTargets.transform;
			newCircleTarget.transform.localPosition = newCircleTargetPosition;
			newCircleTarget.transform.forward = player.transform.forward;
			newCircleTarget.transform.localScale = new Vector3(targetScale, targetScale, targetScale);

			circleTargetPoints.Add(newCircleTargetPosition);
		}
	}

	private void GenerateSquareTargets() {
		int x = -1;
		int y = -1;

		//point to the right
		int dx = 1;
		int dy = 0;

		for (int side = 0; side < 4; ++side)
		{
			for (int i = 1; i < 10; ++i)
			{
				Vector3 newCircleTargetPosition = new Vector3( x, y );

				GameObject newCircleTarget = Instantiate(targetPrefab);
				newCircleTarget.transform.parent = circleTargets.transform;
				newCircleTarget.transform.localPosition = newCircleTargetPosition;
				newCircleTarget.transform.localScale = new Vector3(targetScale, targetScale, targetScale);

				circleTargetPoints.Add(newCircleTargetPosition);

				x += dx;
				y += dy;
			}
			//turn right
			int t = dx;
			dx = -dy;
			dy = t;
		}

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

	private void CheckCircle( Vector2 newCollisionPoint )
    {
		failedAtCircle = true;

		for (int i = 0; i < circleTargetPoints.Count; i++)
		{
			//bool testBool = false;
			if (Vector2.Distance(circleTargetPoints[i], newCollisionPoint) < 0.2f)
			{
				failedAtCircle = false;
				if (!hitCircleTargetPoints.Contains(circleTargetPoints[i]))
				{
					hitCircleTargetPoints.Add(circleTargetPoints[i]);
				}
			}
		}

		if ( failedAtCircle )
		{
			//circleTargets.SetActive(false);
		}
	}

	public void Pinch()
	{
		hitAllCircleTargets = true;
		failedAtCircle = false;
		pinching = true;
	}

	public void EndPinch()
	{
		foreach (Vector2 circleTargetPoint in circleTargetPoints)
		{
			if (!hitCircleTargetPoints.Contains(circleTargetPoint))
			{
				hitAllCircleTargets = false;
				break;
			}
		}

		if (hitAllCircleTargets && !failedAtCircle)
		{
			FireSpell( Shape.CIRCLE );
		}

		drawPoints.Clear();
		collisionPoints.Clear();
		hitCircleTargetPoints.Clear();
		pinching = false;

		circleTargets.SetActive(false);
		squareTargets.SetActive(false);
		triangeTargets.SetActive(false);
	}
}
