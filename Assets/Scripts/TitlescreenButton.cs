using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlescreenButton : MonoBehaviour {

	private bool shouldLoadGameplayScene = false;

	private float timer = 0.0f;

	[SerializeField] float loadSceneCountdown;

	public void PlayButtonClicked() {
		shouldLoadGameplayScene = true;
	}

	public void QuitButtonClicked() {
		Application.Quit();
	}

	private void Update() {
		if ( shouldLoadGameplayScene ) {
			timer += Time.deltaTime;

			if ( timer >= loadSceneCountdown ) {
				SceneManager.LoadScene( "Assets/Scenes/WhiteboxScene(Transferral).unity" );
			}
		}
	}

}
