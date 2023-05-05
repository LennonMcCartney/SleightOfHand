using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCanvas : MonoBehaviour {

	[SerializeField] private GameObject heartPrefab;

	[SerializeField] private Player player;

	[SerializeField] private List<GameObject> hearts;

	private void Start() {
		
		for ( int i = 0; i < player.HitPoints; i++ ) {
			GameObject newHeart = Instantiate( heartPrefab, transform );
			hearts.Add( newHeart );
		}

	}

	public void RemoveHeart() {
		Destroy( hearts[ hearts.Count - 1 ] );
		hearts.RemoveAt( hearts.Count - 1 );
	}

}
