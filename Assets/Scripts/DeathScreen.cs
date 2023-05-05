using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour {

	private Image image;

	private bool playDeathScreen = false;

	private void Start() {
		image = GetComponent<Image>();
	}

	public void PlayDeathScreen() {
		playDeathScreen = true;
	}

	private void Update() {
		if ( playDeathScreen && image.rectTransform.position.y > 380 ) {
			Vector3 newPosition = image.rectTransform.position;
			newPosition.y -= Time.deltaTime * 500.0f;
			image.rectTransform.position = newPosition;
		}
	}

}
