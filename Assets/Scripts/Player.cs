using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] public VirtualCameraController virtualCameraController;

	[SerializeField] private float speed;

	private void Start() {
		virtualCameraController.speed = speed;
	}

}
