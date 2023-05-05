using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[field:SerializeField]
	public VirtualCameraController VirtualCameraController { get; set; }

	[field: SerializeField]
	public CinemachineVirtualCamera CinemachineVirtualCamera { get; set; }

	[SerializeField] private HeartCanvas heartCanvas;

	[field: SerializeField] public int HitPoints { get; set; } = 10;

	[SerializeField] private float speed;

	private void Start() {
		VirtualCameraController.speed = speed;
	}

	public void Hit() {
		if ( HitPoints > 0 ) {
			HitPoints--;
			heartCanvas.RemoveHeart();

			if ( HitPoints <= 0 ) {
				Debug.Log( "Player dead" );
			}
		}
	}

}
