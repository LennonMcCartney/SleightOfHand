using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[field:SerializeField]
	public VirtualCameraController VirtualCameraController { get; set; }

	[field: SerializeField]
	public CinemachineVirtualCamera CinemachineVirtualCamera { get; set; }

	[SerializeField] private float speed;

	private void Start() {
		VirtualCameraController.speed = speed;
	}

}
