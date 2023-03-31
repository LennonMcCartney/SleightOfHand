using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour {

	CinemachineVirtualCamera virtualCamera;

	CinemachineTrackedDolly trackedDolly;

	[SerializeField] float speed;

	private void Start() {
		virtualCamera = GetComponent<CinemachineVirtualCamera>();

		trackedDolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();

	}

	private void Update() {
		trackedDolly.m_PathPosition += Time.deltaTime * speed;

	}

}
