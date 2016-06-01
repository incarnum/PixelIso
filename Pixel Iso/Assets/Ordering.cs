using UnityEngine;
using System.Collections;

public class Ordering : MonoBehaviour {
	// Use this for initialization
	BoxCollider2D Player;
	SpriteRenderer sprite;
	void Start () {
		Player = GameObject.Find ("Player").GetComponent<BoxCollider2D> ();
		sprite = GetComponent<SpriteRenderer> ();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other == Player) {
			sprite.sortingOrder = 2;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other == Player) {
			sprite.sortingOrder = 0;
		}
	}
}
