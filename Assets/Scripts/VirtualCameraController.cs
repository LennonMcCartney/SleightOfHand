using Cinemachine;
using LeapInternal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour {

	[HideInInspector] public bool shouldMove = true;

	private Enemy[] enemies;

	private CinemachineVirtualCamera virtualCamera;

	private CinemachineTrackedDolly trackedDolly;

	[HideInInspector] public float speed;

	private void Start() {
		virtualCamera = GetComponent<CinemachineVirtualCamera>();

		trackedDolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();

	}

	private void Update() {

		if ( shouldMove ) {
			trackedDolly.m_PathPosition += Time.deltaTime * speed;
		}

		Enemy closestEnemy = GetClosestEnemy();
		if ( closestEnemy ) {
			virtualCamera.LookAt = GetClosestEnemy().transform;

		}

	}

	private Enemy GetClosestEnemy() {

		enemies = FindObjectsOfType<Enemy>();

		Enemy closestEnemy = null;

		float closestDistanceSquared = Mathf.Infinity;

		foreach ( Enemy enemy in enemies ) {

			float distanceSquared = Vector3.Distance( enemy.transform.position, transform.position );

			if ( distanceSquared < closestDistanceSquared ) {
				closestDistanceSquared = distanceSquared;
				closestEnemy = enemy;
			}

		}

		return closestEnemy;

	}

}
