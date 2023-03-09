using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shape
{
	CIRCLE,
	SQUARE,
	TRIANGLE
}

public class Enemy : MonoBehaviour
{
    public void Hit() {
		Destroy(gameObject);
	}
}
